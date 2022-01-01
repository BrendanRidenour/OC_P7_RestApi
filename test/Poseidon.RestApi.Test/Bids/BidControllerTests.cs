using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Poseidon.RestApi.Internal;
using System.Linq;
using Xunit;
using static Poseidon.RestApi.TestHelpers;

namespace Poseidon.RestApi.Bids
{
    public class BidControllerTests
    {
        [Fact]
        public void InheritsAbstractValidator()
        {
            Assert.True(typeof(EntityControllerBase<BidEntity>)
                .IsAssignableFrom(typeof(BidController)));
        }

        [Fact]
        public void HasAuthorizeAttribute()
        {
            var attribute = GetClassAttribute<BidController, AuthorizeAttribute>();

            Assert.NotNull(attribute);
        }

        [Fact]
        public void HasRouteAttribute()
        {
            var attributes = GetClassAttributes<BidController, RouteAttribute>();

            Assert.NotNull(attributes);
            Assert.Equal("BidList", attributes.First().Template);
        }
    }
}