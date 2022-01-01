using Microsoft.AspNetCore.Authorization;
using Poseidon.RestApi.Internal;
using Xunit;
using static Poseidon.RestApi.TestHelpers;

namespace Poseidon.RestApi.Rules
{
    public class RuleControllerTests
    {
        [Fact]
        public void HasAuthorizeAttribute()
        {
            var attribute = GetClassAttribute<RuleController, AuthorizeAttribute>();

            Assert.NotNull(attribute);
        }

        [Fact]
        public void InheritsAbstractValidator()
        {
            Assert.True(typeof(EntityControllerBase<RuleEntity>)
                .IsAssignableFrom(typeof(RuleController)));
        }
    }
}