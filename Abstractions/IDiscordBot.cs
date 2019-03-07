using DashBot.Entities;

namespace DashBot.Abstractions
{
    public interface IDiscordBot
    {
        BotAccount Account { get; set; }
        BotStatus GetStatus();
        void Connect();
        void Stop();
    }
}