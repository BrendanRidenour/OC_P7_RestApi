using System.ComponentModel.DataAnnotations;
using Xunit;
using static Poseidon.RestApi.TestHelpers;

namespace Poseidon.RestApi.Internal
{
    public class TradeEntityBaseTests
    {
        [Fact]
        public void InheritsEntityBase()
        {
            Assert.True(typeof(EntityBase).IsAssignableFrom(typeof(TradeEntityBase)));
        }

        [Fact]
        public void AccountProperty_HasRequiredAttribute()
        {
            var attribute = GetPropertyAttribute<TradeEntityBase, RequiredAttribute>(
                "Account");

            Assert.NotNull(attribute);
        }

        [Fact]
        public void TypeProperty_HasRequiredAttribute()
        {
            var attribute = GetPropertyAttribute<TradeEntityBase, RequiredAttribute>(
                "Type");

            Assert.NotNull(attribute);
        }
    }
}