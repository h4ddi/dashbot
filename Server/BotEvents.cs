using System;
using DashBot.Abstractions;
using DashBot.Entities;
using Microsoft.AspNetCore.SignalR;
using Server.Hubs;

namespace Server
{
    public class BotEvents
    {
        private readonly IHubContext<BotHub> _botHubContext;

        public BotEvents(ILogger logger, IDiscordBot bot, IHubContext<BotHub> botHubContext)
        {
            _botHubContext = botHubContext;

            logger.OnLog += OnNewLog;
            bot.OnConnectedChanged += BotOnConnectedChanged;
            bot.OnBotAccountChanged += OnBotAccountChanged;
        }

        private async void OnNewLog(object sender, EventArgs e)
        {
            if (!(e is LogEventArgs args)) { return; }
            await _botHubContext.Clients.All.SendCoreAsync("NewLogAdded", new object[] { args.NewMessage });
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
