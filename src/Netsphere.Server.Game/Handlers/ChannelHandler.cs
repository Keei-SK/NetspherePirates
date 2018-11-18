using System.Linq;
using System.Threading.Tasks;
using ExpressMapper.Extensions;
using Microsoft.Extensions.Logging;
using Netsphere.Network;
using Netsphere.Network.Data.Game;
using Netsphere.Network.Message.Game;
using Netsphere.Server.Game.Rules;
using Netsphere.Server.Game.Services;
using ProudNet;

namespace Netsphere.Server.Game.Handlers
{
    internal class ChannelHandler
        : IHandle<CGetChannelInfoReqMessage>, IHandle<CChannelEnterReqMessage>, IHandle<CChannelLeaveReqMessage>
    {
        private readonly ChannelService _channelService;

        public ChannelHandler(ChannelService channelService)
        {
            _channelService = channelService;
        }

        [Firewall(typeof(MustBeLoggedIn))]
        public async Task<bool> OnHandle(MessageContext context, CGetChannelInfoReqMessage message)
        {
            var session = context.GetSession<Session>();
            var plr = session.Player;

            switch (message.Request)
            {
                case ChannelInfoRequest.RoomList:
                case ChannelInfoRequest.RoomList2:
                    // TODO Room list
                    break;

                case ChannelInfoRequest.ChannelList:
                    if (plr.Channel == null)
                    {
                        var channels = _channelService.Select(x => x.Map<Channel, ChannelInfoDto>()).ToArray();
                        await session.SendAsync(new SChannelListInfoAckMessage(channels));
                    }

                    break;

                default:
                    plr.Logger.LogWarning("Invalid channel info request {Request}", message.Request);
                    break;
            }

            return true;
        }

        [Firewall(typeof(MustBeInChannel), Invert = true)]
        public async Task<bool> OnHandle(MessageContext context, CChannelEnterReqMessage message)
        {
            var session = context.GetSession<Session>();
            var plr = session.Player;

            var channel = _channelService[message.Channel];
            if (channel == null)
            {
                await session.SendAsync(new SServerResultInfoAckMessage(ServerResult.NonExistingChannel));
                return true;
            }

            var result = channel.Join(plr);
            switch (result)
            {
                case ChannelJoinError.OK:
                    await plr.Session.SendAsync(new SServerResultInfoAckMessage(ServerResult.ChannelEnter));
                    break;

                case ChannelJoinError.AlreadyInChannel:
                    await plr.Session.SendAsync(new SServerResultInfoAckMessage(ServerResult.JoinChannelFailed));
                    break;

                case ChannelJoinError.ChannelFull:
                    await plr.Session.SendAsync(new SServerResultInfoAckMessage(ServerResult.ChannelLimitReached));
                    break;
            }

            return true;
        }

        [Firewall(typeof(MustBeInChannel))]
        public async Task<bool> OnHandle(MessageContext context, CChannelLeaveReqMessage message)
        {
            var session = context.GetSession<Session>();
            var plr = session.Player;

            plr.Channel.Leave(plr);
            await plr.Session.SendAsync(new SServerResultInfoAckMessage(ServerResult.ChannelLeave));
            return true;
        }
    }
}