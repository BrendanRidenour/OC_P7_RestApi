using Poseidon.RestApi.Internal;
using Xunit;

namespace Poseidon.RestApi.Trades
{
    public class TradeControllerTests
    {
        [Fact]
        public void InheritsAbstractValidator()
        {
            Assert.True(typeof(EntityControllerBase<TradeEntity>)
                .IsAssignableFrom(typeof(TradeController)));
        }
    }
}