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
        private readonly IHubContext<ChatHub> _chatHubContext;

        public BotEvents(ILogger logger, IDiscordBot bot, IHubContext<BotHub> botHubContext, IHubContext<ChatHub> chatHubContext)
        {
            _botHubContext = botHubContext;
            _chatHubContext = chatHubContext;

            logger.OnLog += OnNewLog;
            bot.OnConnectedChanged += BotOnConnectedChanged;
            bot.OnBotAccountChanged += OnBotAccountChanged;
            bot.OnBotReceivedMessage += OnBotReceivedMessage;
        }

        private async void OnBotReceivedMessage(object sender, EventArgs e)
        {
            if (!(e is ReceivedMessageEventArgs args)) { return; }
            await _chatHubContext.Clients.All.SendCoreAsync($"Chat-{args.ChannelId}", new object[] { args });
            await _chatHubContext.Clients.All.SendCoreAsync("ChatGlobal", new object[] { args });
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
