using Microsoft.AspNetCore.Mvc.ModelBinding;
using Xunit;
using static Poseidon.RestApi.TestHelpers;

namespace Poseidon.RestApi.Internal
{
    public class EntityBaseTests
    {
        [Fact]
        public void IdPropertyHasBindNeverAttribute()
        {
            var attribute = GetPropertyAttribute<EntityBase, BindNeverAttribute>("Id");

            Assert.NotNull(attribute);
        }
    }
}