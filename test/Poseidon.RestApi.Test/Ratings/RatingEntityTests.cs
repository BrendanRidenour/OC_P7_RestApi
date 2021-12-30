using Poseidon.RestApi.Internal;
using Xunit;

namespace Poseidon.RestApi.Ratings
{
    public class RatingEntityTests
    {
        [Fact]
        public void InheritsEntityBase()
        {
            Assert.True(typeof(EntityBase).IsAssignableFrom(typeof(RatingEntity)));
        }
    }
}