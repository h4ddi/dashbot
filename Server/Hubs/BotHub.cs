using System.Threading.Tasks;
using DashBot.Abstractions;
using Microsoft.AspNetCore.SignalR;

namespace Server.Hubs
{
    public class BotHub : Hub
    {
        private readonly IDiscordBot _bot;

        public BotHub(IDiscordBot bot)
        {
            _bot = bot;
        }

        public Task ToggleBotConnection()
        {
            if(_bot.IsRunning())
            {
                _bot.Stop();
            }
            else
            {
                _bot.Connect();
            }

            return Task.CompletedTask;
        }
    }
}
