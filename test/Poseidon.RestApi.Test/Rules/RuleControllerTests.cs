using Poseidon.RestApi.Internal;
using Xunit;

namespace Poseidon.RestApi.Rules
{
    public class RuleControllerTests
    {
        [Fact]
        public void InheritsAbstractValidator()
        {
            Assert.True(typeof(EntityControllerBase<RuleEntity>)
                .IsAssignableFrom(typeof(RuleController)));
        }
    }
}