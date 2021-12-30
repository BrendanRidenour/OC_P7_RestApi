using Poseidon.RestApi.Internal;
using System.Text.Json.Serialization;
using Xunit;

namespace Poseidon.RestApi.Trades
{
    public class TradeEntityTests
    {
        [Fact]
        public void InheritsTradeEntityBase()
        {
            Assert.True(typeof(TradeEntityBase).IsAssignableFrom(typeof(TradeEntity)));
        }

        [Fact]
        public void IdPropertyHasJsonPropertyName()
        {
            var attribute = TestHelpers.GetPropertyAttribute<TradeEntity, JsonPropertyNameAttribute>(
                nameof(TradeEntity.Id));

            Assert.NotNull(attribute);
            Assert.Equal("TradeId", attribute.Name);
        }
    }
}