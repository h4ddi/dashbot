using DashBot.Abstractions;
using DashBot.Entities;
using Discord;
using Discord.WebSocket;

namespace DashBot.Bot
{
    public class DiscordBot : IDiscordBot
    {
        public BotAccount Account { get; set; }
        private DiscordSocketClient _client;

        public DiscordBot()
        {
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Debug
            });
        }
        
        public BotStatus GetStatus()
        {
            if (_client.ConnectionState == ConnectionState.Disconnected ||
                _client.ConnectionState == ConnectionState.Disconnected)
            {
                return BotStatus.NotRunning;
            }

            return BotStatus.Running;
        }

        public async void Connect()
        {
            if (Account is null) { return; }

            if (GetStatus() == BotStatus.Running)
            {
                await _client.StopAsync();
            }

            await _client.LoginAsync(TokenType.Bot, Account.Token);
            await _client.StartAsync();
        }

        public async void Stop()
            => await _client.StopAsync();
    }
}
