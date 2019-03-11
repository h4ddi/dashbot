using System;
using DashBot.Abstractions;
using DashBot.Entities;
using Microsoft.AspNetCore.SignalR;
using Server.Hubs;

namespace Server
{
    public class BotEvents
    {
        private readonly IDiscordBot _bot;
        private readonly IHubContext<BotHub> _botHubContext;

        public BotEvents(IDiscordBot bot, IHubContext<BotHub> botHubContext)
        {
            _bot = bot;
            _botHubContext = botHubContext;

            _bot.OnConnectedChanged += BotOnConnectedChanged;
            _bot.OnBotAccountChanged += OnBotAccountChanged;
        }

        private async void BotOnConnectedChanged(object sender, EventArgs e)
        {
            if (!(e is ConnectionEventArgs args)) { return; }
            await _botHubContext.Clients.All.SendCoreAsync("BotConnectedChanged", new object[] { args.IsConnected });
        }

        private async void OnBotAccountChanged(object sender, EventArgs e)
        {
            if (!(e is BotAccountEventArgs account)) { return; }
            await _botHubContext.Clients.All.SendCoreAsync("BotAccountSelected", new object[] {account});
        }
    }
}
