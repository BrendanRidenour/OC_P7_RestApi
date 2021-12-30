using Xunit;

namespace Poseidon.RestApi.Internal
{
    public class TradeEntityBaseTests
    {
        [Fact]
        public void InheritsEntityBase()
        {
            Assert.True(typeof(EntityBase).IsAssignableFrom(typeof(TradeEntityBase)));
        }
    }
}