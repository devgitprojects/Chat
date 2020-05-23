using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Logging
{
    internal class FileLogger : ILogger
    {
        private readonly string path;

        public FileLogger(string categoryName, string logFilePath)
        {
            path = logFilePath;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId,
                TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            string msg = $"{DateTime.Now} {logLevel} {formatter(state, exception)}";
            File.AppendAllText(path, msg);
        }
    }
}
