using System.Security.Claims;

namespace Poseidon.RestApi.Mocks
{
    public class MockClaimsIdentity : ClaimsIdentity
    {
        public override bool IsAuthenticated { get; }

        public MockClaimsIdentity(string authenticationType,
            bool isAuthenticated)
            : base(authenticationType)
        {
            this.IsAuthenticated = isAuthenticated;
        }
    }
}