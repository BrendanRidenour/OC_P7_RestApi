using Poseidon.RestApi.Internal;
using Xunit;

namespace Poseidon.RestApi.Users
{
    public class UserEntityTests
    {
        [Fact]
        public void InheritsEntityBase()
        {
            Assert.True(typeof(EntityBase).IsAssignableFrom(typeof(UserEntity)));
        }
    }
}