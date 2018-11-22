using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Netsphere.Database;
using ProudNet.Hosting.Services;

namespace Netsphere.Server.Game.Services
{
    public class PlayerSaveService : IHostedService
    {
        private readonly ILogger _logger;
        private readonly AppOptions _appOptions;
        private readonly ISchedulerService _schedulerService;
        private readonly IDatabaseProvider _databaseProvider;
        private readonly PlayerManager _playerManager;
        private readonly CancellationTokenSource _shutdown;

        public PlayerSaveService(ILogger<PlayerSaveService> logger, IOptions<AppOptions> appOptions,
            ISchedulerService schedulerService, IDatabaseProvider databaseProvider, PlayerManager playerManager)
        {
            _logger = logger;
            _appOptions = appOptions.Value;
            _schedulerService = schedulerService;
            _databaseProvider = databaseProvider;
            _playerManager = playerManager;
            _shutdown = new CancellationTokenSource();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _schedulerService.ScheduleAsync(OnSave, null, null, _appOptions.SaveInterval, _shutdown.Token);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _shutdown.Cancel();
            return Task.CompletedTask;
        }

        private async void OnSave(object _, object __)
        {
            if (_shutdown.IsCancellationRequested)
                return;

            await SavePlayers();

            if (_shutdown.IsCancellationRequested)
                return;

            try
            {
                await _schedulerService.ScheduleAsync(OnSave, null, null, _appOptions.SaveInterval, _shutdown.Token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to schedule next save");
            }
        }

        private async Task SavePlayers()
        {
            _logger.LogInformation("Saving players...");
            using (var db = _databaseProvider.Open<GameContext>())
            {
                foreach (var plr in _playerManager)
                {
                    try
                    {
                        if (plr.Session.IsConnected)
                            await plr.Save(db);
                    }
                    catch (Exception ex)
                    {
                        using (plr.AddContextToLogger(_logger))
                            _logger.LogError(ex, "Unable to save player");
                    }
                }
            }
        }
    }
}