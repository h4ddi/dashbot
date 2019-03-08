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
        
        public bool IsRunning() =>
            _client.ConnectionState == ConnectionState.Connected ||
            _client.ConnectionState == ConnectionState.Connecting;

        public async void Connect()
        {
            if (Account is null) { return; }
            if (IsRunning()) { await _client.StopAsync(); }

            await _client.LoginAsync(TokenType.Bot, Account.Token);
            await _client.StartAsync();
        }

        public async void Stop()
            => await _client.StopAsync();
    }
}
