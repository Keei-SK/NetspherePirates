using System;
using System.Threading.Tasks;
using BlubLib;
using Netsphere.Network.Message.Game;
using Netsphere.Server.Game.Rules;
using Netsphere.Server.Game.Services;
using ProudNet;

namespace Netsphere.Server.Game.Handlers
{
    internal class MiscHandler
        : IHandle<CTimeSyncReqMessage>, IHandle<CAdminShowWindowReqMessage>, IHandle<CAdminActionReqMessage>
    {
        private readonly CommandService _commandService;

        public MiscHandler(CommandService commandService)
        {
            _commandService = commandService;
        }

        [Inline]
        public async Task<bool> OnHandle(MessageContext context, CTimeSyncReqMessage message)
        {
            context.Session.Send(new STimeSyncAckMessage
            {
                ClientTime = message.Time,
                ServerTime = (uint)Environment.TickCount
            });

            return true;
        }

        [Firewall(typeof(MustBeLoggedIn))]
        [Inline]
        public async Task<bool> OnHandle(MessageContext context, CAdminShowWindowReqMessage message)
        {
            var session = context.GetSession<Session>();
            var plr = session.Player;

            session.Send(new SAdminShowWindowAckMessage(plr.Account.SecurityLevel <= SecurityLevel.User));
            return true;
        }

        [Firewall(typeof(MustBeLoggedIn))]
        [Inline]
        public async Task<bool> OnHandle(MessageContext context, CAdminActionReqMessage message)
        {
            var session = context.GetSession<Session>();
            var plr = session.Player;

            await _commandService.Execute(plr, message.Command.GetArgs());
            return true;
        }
    }
}
