using Poseidon.RestApi.Internal;
using Xunit;

namespace Poseidon.RestApi.CurvePoints
{
    public class CurvePointControllerTests
    {
        [Fact]
        public void InheritsAbstractValidator()
        {
            Assert.True(typeof(EntityControllerBase<CurvePointEntity>)
                .IsAssignableFrom(typeof(CurvePointController)));
        }
    }
}