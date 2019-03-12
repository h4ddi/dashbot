using System;

namespace DashBot.Entities
{
    public class LogEventArgs : EventArgs
    {
        public string NewMessage { get; set; }
    }
}
