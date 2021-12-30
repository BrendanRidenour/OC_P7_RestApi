using Poseidon.RestApi.Internal;
using Xunit;

namespace Poseidon.RestApi.CurvePoints
{
    public class CurvePointEntityTests
    {
        [Fact]
        public void InheritsEntityBase()
        {
            Assert.True(typeof(EntityBase)
                .IsAssignableFrom(typeof(CurvePointEntity)));
        }
    }
}