using System;
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

        [Theory]
        [InlineData(1, 2)]
        [InlineData(3, 4)]
        public void Copy_WhenCalled_SetsProperties(int idA, int idB)
        {
            var now = DateTimeOffset.UtcNow;
            var entityA = Entity(idA, now);
            var entityB = Entity(idB, now.AddHours(1));

            entityA.CopyProperties(entityB);

            Assert.NotEqual(entityB.Id, entityA.Id);
            Assert.Equal(entityB.Account, entityA.Account);
            Assert.Equal(entityB.Type, entityA.Type);
            Assert.Equal(entityB.Benchmark, entityA.Benchmark);
            Assert.Equal(entityB.Security, entityA.Security);
            Assert.Equal(entityB.Status, entityA.Status);
            Assert.Equal(entityB.Trader, entityA.Trader);
            Assert.Equal(entityB.Book, entityA.Book);
            Assert.Equal(entityB.CreationName, entityA.CreationName);
            Assert.Equal(entityB.CreationDate, entityA.CreationDate);
            Assert.Equal(entityB.RevisionName, entityA.RevisionName);
            Assert.Equal(entityB.RevisionDate, entityA.RevisionDate);
            Assert.Equal(entityB.DealName, entityA.DealName);
            Assert.Equal(entityB.DealType, entityA.DealType);
            Assert.Equal(entityB.SourceListId, entityA.SourceListId);
            Assert.Equal(entityB.Side, entityA.Side);
        }

        private static TestTradeEntityBase Entity(int id, DateTimeOffset dateTime) =>
            new TestTradeEntityBase()
            {
                Id = id,
                Account = $"account{id}",
                Type = $"type{id}",
                Benchmark = $"benchmark{id}",
                Security = $"security{id}",
                Status = $"status{id}",
                Trader = $"trader{id}",
                Book = $"book{id}",
                CreationName = $"creation{id}",
                CreationDate = dateTime,
                RevisionName = $"revision{id}",
                RevisionDate = dateTime,
                DealName = $"dealname{id}",
                DealType = $"dealtype{id}",
                SourceListId = $"sourcelistid{id}",
                Side = $"side{id}",
            };
        private class TestTradeEntityBase : TradeEntityBase
        {
            new public void CopyProperties(TradeEntityBase entity) => base.CopyProperties(entity);
        }
    }
}