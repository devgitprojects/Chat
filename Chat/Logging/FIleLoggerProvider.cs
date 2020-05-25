using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Logging
{
    class FileLoggerProvider : ILoggerProvider
    {
        private readonly string path;

        public FileLoggerProvider(string logFilePath)
        {
            path = logFilePath;
        }
        public string FilePath { get; set; }
        public ILogger CreateLogger(string categoryName) => new FileLogger(categoryName, path);

        public void Dispose() { }
    }
}
