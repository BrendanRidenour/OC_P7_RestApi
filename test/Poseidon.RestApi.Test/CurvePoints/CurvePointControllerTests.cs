using Microsoft.AspNetCore.Authorization;
using Poseidon.RestApi.Internal;
using Xunit;
using static Poseidon.RestApi.TestHelpers;

namespace Poseidon.RestApi.CurvePoints
{
    public class CurvePointControllerTests
    {
        [Fact]
        public void HasAuthorizeAttribute()
        {
            var attribute = GetClassAttribute<CurvePointController, AuthorizeAttribute>();

            Assert.NotNull(attribute);
            Assert.Equal("Admin", attribute.Roles);
        }

        [Fact]
        public void InheritsAbstractValidator()
        {
            Assert.True(typeof(EntityControllerBase<CurvePointEntity>)
                .IsAssignableFrom(typeof(CurvePointController)));
        }
    }
}