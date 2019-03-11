using System;
using DashBot.Entities;

namespace DashBot.Abstractions
{
    public interface IDiscordBot
    {
        event EventHandler OnConnectedChanged;
        BotAccount Account { get; set; }
        bool IsRunning();
        void Connect();
        void Stop();
    }
}