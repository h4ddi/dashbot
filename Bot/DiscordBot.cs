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
        public event EventHandler OnBotAccountChanged;

        private BotAccount _account;
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

        public BotAccount GetActiveBotAccount() => _account;

        public void SetActiveBotAccount(BotAccount account)
        {
            if (IsRunning()) { return; }
            _account = account;
            BotAccountChanged();
        }

        public bool IsRunning() =>
            _client.ConnectionState == ConnectionState.Connected ||
            _client.ConnectionState == ConnectionState.Connecting;

        public async void Connect()
        {
            if (_account is null || IsRunning()) { return; }

            await _client.LoginAsync(TokenType.Bot, _account.Token);
            await _client.StartAsync();
        }

        public async void Stop()
            => await _client.StopAsync();

        private void BotAccountChanged()
        {
            OnBotAccountChanged?.Invoke(this, new BotAccountEventArgs
            {
                AvatarUrl = _account.AvatarUrl,
                Name = _account.Name
            });
        }
    }
}
