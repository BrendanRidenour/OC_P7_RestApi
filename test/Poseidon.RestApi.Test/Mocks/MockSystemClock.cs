using Microsoft.Extensions.Internal;
using System;

namespace Poseidon.RestApi.Mocks
{
    public class MockSystemClock : ISystemClock
    {
        public DateTimeOffset UtcNow { get; }

        public MockSystemClock(DateTimeOffset utcNow)
        {
            UtcNow = utcNow;
        }
    }
}