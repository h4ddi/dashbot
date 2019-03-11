using System;

namespace DashBot.Entities
{
    public class BotAccountEventArgs : EventArgs
    {
        public string AvatarUrl { get; set; }
        public string Name { get; set; }
    }
}
