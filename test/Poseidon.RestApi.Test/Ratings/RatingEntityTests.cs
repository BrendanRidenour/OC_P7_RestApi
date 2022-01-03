using Poseidon.RestApi.Internal;
using System;
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

        [Theory]
        [InlineData(1, 2)]
        [InlineData(3, 4)]
        public void CopyProperties_WhenCalled_SetsProperties(int idA, int idB)
        {
            var entityA = Entity(idA);
            var entityB = Entity(idB);

            entityA.CopyProperties(entityB);

            Assert.NotEqual(entityB.Id, entityA.Id);
            Assert.Equal(entityB.MoodysRating, entityA.MoodysRating);
            Assert.Equal(entityB.SandPRating, entityA.SandPRating);
            Assert.Equal(entityB.FitchRating, entityA.FitchRating);
            Assert.Equal(entityB.OrderNumber, entityA.OrderNumber);
        }

        private static RatingEntity Entity(int id) =>
            new RatingEntity()
            {
                Id = id,
                MoodysRating = $"moodys{id}",
                SandPRating = $"sandp{id}",
                FitchRating = $"fitch{id}",
                OrderNumber = id,
            };
    }
}