using Poseidon.RestApi.Internal;
using Xunit;

namespace Poseidon.RestApi.Users
{
    public class UserControllerTests
    {
        [Fact]
        public void InheritsAbstractValidator()
        {
            Assert.True(typeof(EntityControllerBase<UserEntity>)
                .IsAssignableFrom(typeof(UserController)));
        }
    }
}