using System;

namespace DashBot.Entities
{
    public class ConnectionEventArgs : EventArgs
    {
        public bool IsConnected { get; set; }
    }
}
