using DashBot.Entities;

namespace DashBot.Abstractions
{
    public interface IDiscordBot
    {
        BotAccount Account { get; set; }
        bool IsRunning();
        void Connect();
        void Stop();
    }
}