using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace Chat.Logging
{
    internal class FileLogger : ILogger
    {
        private readonly string path; 
        private readonly string category;
        private static object _lock = new object();
        public FileLogger(string categoryName, string logFilePath)
        {
            path = logFilePath;
            category = categoryName;
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
            if (formatter != null)
            {
                lock (_lock)
                {
                    string msg = $"{DateTime.Now} {category} {logLevel} {formatter(state, exception)} {Environment.NewLine}";
                    File.AppendAllText(path, msg);
                }
            }

        }
    }
}
