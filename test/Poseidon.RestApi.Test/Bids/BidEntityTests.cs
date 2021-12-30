using Poseidon.RestApi.Internal;
using System.Text.Json.Serialization;
using Xunit;

namespace Poseidon.RestApi.Bids
{
    public class BidEntityTests
    {
        [Fact]
        public void InheritsTradeEntityBase()
        {
            Assert.True(typeof(TradeEntityBase).IsAssignableFrom(typeof(BidEntity)));
        }

        [Fact]
        public void IdPropertyHasJsonPropertyName()
        {
            var attribute = TestHelpers.GetPropertyAttribute<BidEntity, JsonPropertyNameAttribute>(
                nameof(BidEntity.Id));

            Assert.NotNull(attribute);
            Assert.Equal("BidListId", attribute.Name);
        }
    }
}