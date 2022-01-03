using Microsoft.Extensions.Logging;
using System;

namespace Poseidon.RestApi.Mocks
{
    public class MockLogger<T> : ILogger<T>
    {
        public LogLevel? Log_InputLogLevel;
        public string? Log_WrittenString;
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            this.Log_InputLogLevel = logLevel;
            this.Log_WrittenString = state!.ToString();
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            throw new NotImplementedException();
        }
    }
}