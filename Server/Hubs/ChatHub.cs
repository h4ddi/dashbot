using System.Threading.Tasks;
using DashBot.Abstractions;
using Microsoft.AspNetCore.SignalR;

namespace Server.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IDiscordBot _bot;

        public ChatHub(IDiscordBot bot)
        {
            _bot = bot;
        }

        public async Task SayInChannel(string location, string message)
        {
            var locationSplit = location.Split('/');
            if (locationSplit.Length != 2) { return; }
            var serverParsed = ulong.TryParse(locationSplit[0], out var serverId);
            var channelParsed = ulong.TryParse(locationSplit[1], out var channelId);
            if (!serverParsed || !channelParsed) { return; }
            await _bot.SayInChannel(serverId, channelId, message);
        }
    }
}