using Microsoft.AspNetCore.Authorization;
using Poseidon.RestApi.Internal;
using Xunit;
using static Poseidon.RestApi.TestHelpers;

namespace Poseidon.RestApi.Trades
{
    public class TradeControllerTests
    {
        [Fact]
        public void HasAuthorizeAttribute()
        {
            var attribute = GetClassAttribute<TradeController, AuthorizeAttribute>();

            Assert.NotNull(attribute);
        }

        [Fact]
        public void InheritsAbstractValidator()
        {
            Assert.True(typeof(EntityControllerBase<TradeEntity>)
                .IsAssignableFrom(typeof(TradeController)));
        }
    }
}