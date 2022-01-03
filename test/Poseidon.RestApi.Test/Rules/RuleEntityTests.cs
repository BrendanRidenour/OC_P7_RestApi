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

        [Theory]
        [InlineData(1, 2)]
        [InlineData(3, 4)]
        public void CopyProperties_WhenCalled_SetsProperties(int idA, int idB)
        {
            var entityA = Entity(idA);
            var entityB = Entity(idB);

            entityA.CopyProperties(entityB);

            Assert.NotEqual(entityB.Id, entityA.Id);
            Assert.Equal(entityB.Name, entityA.Name);
            Assert.Equal(entityB.Description, entityA.Description);
            Assert.Equal(entityB.Json, entityA.Json);
            Assert.Equal(entityB.Template, entityA.Template);
            Assert.Equal(entityB.SqlStr, entityA.SqlStr);
            Assert.Equal(entityB.SqlPart, entityA.SqlPart);
        }

        private static RuleEntity Entity(int id) =>
            new RuleEntity()
            {
                Id = id,
                Name = $"name{id}",
                Description = $"desc{id}",
                Json = $"json{id}",
                Template = $"temp{id}",
                SqlStr = $"sqls{id}",
                SqlPart = $"sqlp{id}",
            };
    }
}