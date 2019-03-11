using System;
using DashBot.Entities;

namespace DashBot.Abstractions
{
    public interface IDiscordBot
    {
        event EventHandler OnConnectedChanged;
        event EventHandler OnBotAccountChanged;
        BotAccount GetActiveBotAccount();
        void SetActiveBotAccount(BotAccount account);
        bool IsRunning();
        void Connect();
        void Stop();
    }
}