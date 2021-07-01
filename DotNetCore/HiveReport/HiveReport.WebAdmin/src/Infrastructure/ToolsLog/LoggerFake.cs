using Microsoft.Extensions.Logging;
using System;

namespace HiveReport.WebAdmin.Infrastructure.ToolsLog
{
    public class LoggerFake : ILogger
    {
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            //do nothing
        }
    }
}
