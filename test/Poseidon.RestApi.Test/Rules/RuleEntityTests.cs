using Poseidon.RestApi.Internal;
using Xunit;

namespace Poseidon.RestApi.Rules
{
    public class RuleEntityTests
    {
        [Fact]
        public void InheritsEntityBase()
        {
            Assert.True(typeof(EntityBase).IsAssignableFrom(typeof(RuleEntity)));
        }
    }
}