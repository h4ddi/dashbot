﻿using System;
using System.Collections.Generic;
using System.Linq;
using DashBot.Abstractions;
using DashBot.Entities;

namespace Logger
{
    public class InMemoryLogger : ILogger
    {
        private List<string> Logs { get; }

        public InMemoryLogger()
        {
            Logs = new List<string>();
        }

        public event EventHandler OnLog;

        public void Log(string message)
        {
            var entry = $"{DateTime.Now.ToShortDateString()} {DateTime.Now.ToLongTimeString()} : {message}";
            Logs.Add(entry);
            OnLog?.Invoke(this, new LogEventArgs { NewMessage = entry });
        }

        public IEnumerable<string> GetAll()
            => new List<string>(Logs);

        public IEnumerable<string> GetLatest(int count)
            => new List<string>(Logs.TakeLast(count));
    }
}
