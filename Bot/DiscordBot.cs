using System;
using System.Collections.Generic;
using System.Linq;
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
        public event EventHandler OnBotReceivedMessage;

        private BotAccount _account;
        private DiscordSocketClient _client;

        private readonly ILogger _logger;

        public DiscordBot(ILogger logger)
        {
            _logger = logger;

            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Debug,
                AlwaysDownloadUsers = true
            });

            _client.Connected += ClientOnConnected;
            _client.Disconnected += ClientOnDisconnected;
            _client.Log += ClientOnLog;
            _client.MessageReceived += ClientOnMessageReceived;
        }

        private Task ClientOnMessageReceived(SocketMessage msg)
        {
            var args = new ReceivedMessageEventArgs
            {
                Message = msg.Content,
                SenderUsername = msg.Author.Username,
                ChannelName = msg.Channel.Name,
                SenderAvatarUrl = msg.Author.GetAvatarUrl(),
                SenderId = msg.Author.Id,
                ChannelId = msg.Channel.Id,
                SenderReputation = 0 // TODO: Pull when reputation is implemented
            };

            OnBotReceivedMessage?.Invoke(this, args);
            return Task.CompletedTask;
        }

        private Task ClientOnLog(LogMessage log)
        {
            _logger.Log($"[{log.Source}] - {log.Message}");
            return Task.CompletedTask;
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

        public IEnumerable<ServerDetail> GetAvailableServers()
            =>_client.Guilds.Select(guild => new ServerDetail
            {
                Name = guild.Name,
                AvatarUrl = guild.IconUrl,
                Id = guild.Id,
                TextChannels = guild.TextChannels.Select(ToTextChannel)
            });
        
        private static TextChannel ToTextChannel(SocketTextChannel tc)
            => new TextChannel
            {
                Name = tc.Name,
                Id = tc.Id
            };

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

        public ServerDetail GetServerDetailFromId(ulong serverId)
        {
            var server = _client.GetGuild(serverId);
            if (server is null) { return null; }
            return new ServerDetail
            {
                AvatarUrl = server.IconUrl,
                Name = server.Name,
                Id = server.Id,
                TextChannels = server.TextChannels.Select(ToTextChannel)
            };
        }

        public TextChannel GetTextChannelDetailFromId(ulong serverId, ulong channelId)
        {
            var channel = _client.GetGuild(serverId).GetTextChannel(channelId);
            return new TextChannel
            {
                Name = channel.Name,
                Id = channel.Id
            };
        }

        public async Task<IEnumerable<ChatMessage>> GetMessageBufferFor(ulong serverId, ulong channelId)
        {
            var guild = _client.GetGuild(serverId);
            var channel = guild.GetTextChannel(channelId);

            var messages = await channel.GetMessagesAsync(5).FlattenAsync();

            return messages.Select(m => new ChatMessage
            {
                ChannelId = channel.Id,
                ChannelName = channel.Name,
                SenderId = m.Author.Id,
                SenderUsername = m.Author.Username,
                SenderAvatarUrl = m.Author.GetAvatarUrl(),
                SenderReputation = 0, // TODO: Get when implemented
                Message = m.Content
            });
        }

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
