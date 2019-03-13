using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DashBot.Entities;

namespace DashBot.Abstractions
{
    public interface IDiscordBot
    {
        event EventHandler OnConnectedChanged;
        event EventHandler OnBotAccountChanged;
        event EventHandler OnBotReceivedMessage;
        IEnumerable<ServerDetail> GetAvailableServers();
        BotAccount GetActiveBotAccount();
        void SetActiveBotAccount(BotAccount account);
        bool IsRunning();
        void Connect();
        void Stop();
        ServerDetail GetServerDetailFromId(ulong serverId);
        TextChannel GetTextChannelDetailFromId(ulong serverId, ulong channelId);
        Task<IEnumerable<ChatMessage>> GetMessageBufferFor(ulong serverId, ulong channelId);
    }
}