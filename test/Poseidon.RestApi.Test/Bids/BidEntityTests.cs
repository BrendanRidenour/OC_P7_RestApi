using Poseidon.RestApi.Internal;
using System;
using System.Text.Json.Serialization;
using Xunit;

namespace Poseidon.RestApi.Bids
{
    public class BidEntityTests
    {
        [Fact]
        public void InheritsTradeEntityBase()
        {
            Assert.True(typeof(TradeEntityBase).IsAssignableFrom(typeof(BidEntity)));
        }

        [Fact]
        public void IdPropertyHasJsonPropertyName()
        {
            var attribute = TestHelpers.GetPropertyAttribute<BidEntity, JsonPropertyNameAttribute>(
                nameof(BidEntity.Id));

            Assert.NotNull(attribute);
            Assert.Equal("BidListId", attribute.Name);
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
            Assert.Equal(entityB.BidQuantity, entityA.BidQuantity);
            Assert.Equal(entityB.AskQuantity, entityA.AskQuantity);
            Assert.Equal(entityB.Bid, entityA.Bid);
            Assert.Equal(entityB.Ask, entityA.Ask);
            Assert.Equal(entityB.BidListDate, entityA.BidListDate);
            Assert.Equal(entityB.Commentary, entityA.Commentary);
        }

        private static BidEntity Entity(int id, DateTimeOffset dateTime, double dbl) =>
            new BidEntity()
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
                BidQuantity = dbl,
                AskQuantity = dbl,
                Bid = dbl,
                Ask = dbl,
                BidListDate = dateTime,
                Commentary = $"commentary{id}",
            };
    }
}