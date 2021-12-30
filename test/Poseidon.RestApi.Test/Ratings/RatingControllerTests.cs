using Poseidon.RestApi.Internal;
using Xunit;

namespace Poseidon.RestApi.Ratings
{
    public class RatingControllerTests
    {
        [Fact]
        public void InheritsAbstractValidator()
        {
            Assert.True(typeof(EntityControllerBase<RatingEntity>)
                .IsAssignableFrom(typeof(RatingController)));
        }
    }
}