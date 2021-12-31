using System.Security.Principal;

namespace Poseidon.RestApi.Mocks
{
    public class MockNotClaimsIdentity : IIdentity
    {
        public string? AuthenticationType => null;

        public bool IsAuthenticated { get; }

        public string? Name => null;

        public MockNotClaimsIdentity(bool isAuthenticated = false)
        {
            this.IsAuthenticated = isAuthenticated;
        }
    }
}