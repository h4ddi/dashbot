using System;
using System.Threading.Tasks;
using DashBot.Abstractions;
using DashBot.Entities;
using Discord;
using Discord.WebSocket;

namespace DashBot.Bot
{
    public class DiscordBot : IDiscordBot
    {
        public event EventHandler OnConnectedChanged;
        public BotAccount Account { get; set; }
        private DiscordSocketClient _client;

        public DiscordBot()
        {
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Debug
            });

            _client.Connected += ClientOnConnected;
            _client.Disconnected += ClientOnDisconnected;
        }

        private Task ClientOnDisconnected(Exception e)
        {
            ConnectedChanged(false);
            return Task.CompletedTask;
        }

        private Task ClientOnConnected()
        {
            ConnectedChanged(true);
            return Task.CompletedTask;
        }

        private void ConnectedChanged(bool isConnected)
        {
            var e = new ConnectionEventArgs { IsConnected = isConnected };
            OnConnectedChanged?.Invoke(this, e);
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
