using System;
using System.Collections.Generic;
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
    }
}