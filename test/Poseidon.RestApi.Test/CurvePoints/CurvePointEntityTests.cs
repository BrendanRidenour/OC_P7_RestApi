using Poseidon.RestApi.Internal;
using System;
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

        [Theory]
        [InlineData(1, .1, 2, .2)]
        [InlineData(3, .3, 4, .4)]
        public void CopyProperties_WhenCalled_SetsProperties(int idA, double dblA, int idB,
            double dblB)
        {
            var now = DateTimeOffset.UtcNow;
            var entityA = Entity(idA, now, dblA);
            var entityB = Entity(idB, now.AddHours(1), dblB);

            entityA.CopyProperties(entityB);

            Assert.NotEqual(entityB.Id, entityA.Id);
            Assert.Equal(entityB.CurveId, entityA.CurveId);
            Assert.Equal(entityB.AsOfDate, entityA.AsOfDate);
            Assert.Equal(entityB.Term, entityA.Term);
            Assert.Equal(entityB.Value, entityA.Value);
            Assert.Equal(entityB.CreationDate, entityA.CreationDate);
        }

        private static CurvePointEntity Entity(int id, DateTimeOffset dateTime, double dbl) =>
            new CurvePointEntity()
            {
                Id = id,
                CurveId = id,
                AsOfDate = dateTime,
                Term = dbl,
                Value = dbl,
                CreationDate = dateTime,
            };
    }
}