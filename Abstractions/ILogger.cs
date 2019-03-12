using System;
using System.Collections.Generic;

namespace DashBot.Abstractions
{
    public interface ILogger
    {
        event EventHandler OnLog;
        void Log(string message);
        IEnumerable<string> GetAll();
        IEnumerable<string> GetLatest(int count);
    }
}
