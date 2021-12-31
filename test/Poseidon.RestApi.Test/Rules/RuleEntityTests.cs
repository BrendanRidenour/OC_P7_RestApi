using Poseidon.RestApi.Internal;
using System.ComponentModel.DataAnnotations;
using Xunit;
using static Poseidon.RestApi.TestHelpers;

namespace Poseidon.RestApi.Rules
{
    public class RuleEntityTests
    {
        [Fact]
        public void InheritsEntityBase()
        {
            Assert.True(typeof(EntityBase).IsAssignableFrom(typeof(RuleEntity)));
        }

        [Fact]
        public void NameProperty_HasRequiredAttribute()
        {
            var attribute = GetPropertyAttribute<RuleEntity, RequiredAttribute>(
                "Name");

            Assert.NotNull(attribute);
        }
    }
}