using Microsoft.AspNetCore.Authorization;
using Poseidon.RestApi.Internal;
using Xunit;
using static Poseidon.RestApi.TestHelpers;

namespace Poseidon.RestApi.Ratings
{
    public class RatingControllerTests
    {
        [Fact]
        public void HasAuthorizeAttribute()
        {
            var attribute = GetClassAttribute<RatingController, AuthorizeAttribute>();

            Assert.NotNull(attribute);
        }

        [Fact]
        public void InheritsAbstractValidator()
        {
            Assert.True(typeof(EntityControllerBase<RatingEntity>)
                .IsAssignableFrom(typeof(RatingController)));
        }
    }
}