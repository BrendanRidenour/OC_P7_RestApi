using FluentValidation;
using Poseidon.RestApi.Trades;
using System;
using System.Linq;
using Xunit;
using static Poseidon.RestApi.TestHelpers;

namespace Poseidon.RestApi.Internal
{
    public class TradeEntityValidatorBaseTests
    {
        [Fact]
        public void InheritsAbstractValidator()
        {
            Assert.True(typeof(AbstractValidator<TradeEntity>)
                .IsAssignableFrom(typeof(TradeEntityValidatorBase<TradeEntity>)));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void AccountEmpty(string? value)
        {
            var entity = CreateEntity(account: value!);

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(TradeEntity.Account));

            var error = Assert.Single(errors);
            Assert.Equal("AccountEmpty", error.ErrorCode);
        }

        [Fact]
        public void AccountMaxLength30()
        {
            var entity = CreateEntity(account: CreateString(31));

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(TradeEntity.Account));

            var error = Assert.Single(errors);
            Assert.Equal("AccountMaxLength30", error.ErrorCode);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void TypeEmpty(string? value)
        {
            var entity = CreateEntity(type: value!);

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(TradeEntity.Type));

            var error = Assert.Single(errors);
            Assert.Equal("TypeEmpty", error.ErrorCode);
        }

        [Fact]
        public void TypeMaxLength30()
        {
            var entity = CreateEntity(type: CreateString(31));

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(TradeEntity.Type));

            var error = Assert.Single(errors);
            Assert.Equal("TypeMaxLength30", error.ErrorCode);
        }

        [Fact]
        public void BenchmarkMaxLength125()
        {
            var entity = CreateEntity(benchmark: CreateString(126));

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(TradeEntity.Benchmark));

            var error = Assert.Single(errors);
            Assert.Equal("BenchmarkMaxLength125", error.ErrorCode);
        }

        [Fact]
        public void SecurityMaxLength125()
        {
            var entity = CreateEntity(security: CreateString(126));

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(TradeEntity.Security));

            var error = Assert.Single(errors);
            Assert.Equal("SecurityMaxLength125", error.ErrorCode);
        }

        [Fact]
        public void StatusMaxLength10()
        {
            var entity = CreateEntity(status: CreateString(11));

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(TradeEntity.Status));

            var error = Assert.Single(errors);
            Assert.Equal("StatusMaxLength10", error.ErrorCode);
        }

        [Fact]
        public void TraderMaxLength125()
        {
            var entity = CreateEntity(trader: CreateString(126));

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(TradeEntity.Trader));

            var error = Assert.Single(errors);
            Assert.Equal("TraderMaxLength125", error.ErrorCode);
        }

        [Fact]
        public void BookMaxLength125()
        {
            var entity = CreateEntity(book: CreateString(126));

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(TradeEntity.Book));

            var error = Assert.Single(errors);
            Assert.Equal("BookMaxLength125", error.ErrorCode);
        }

        [Fact]
        public void CreationNameMaxLength125()
        {
            var entity = CreateEntity(creationName: CreateString(126));

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(TradeEntity.CreationName));

            var error = Assert.Single(errors);
            Assert.Equal("CreationNameMaxLength125", error.ErrorCode);
        }

        [Fact]
        public void RevisionNameMaxLength125()
        {
            var entity = CreateEntity(revisionName: CreateString(126));

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(TradeEntity.RevisionName));

            var error = Assert.Single(errors);
            Assert.Equal("RevisionNameMaxLength125", error.ErrorCode);
        }

        [Fact]
        public void DealNameMaxLength125()
        {
            var entity = CreateEntity(dealName: CreateString(126));

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(TradeEntity.DealName));

            var error = Assert.Single(errors);
            Assert.Equal("DealNameMaxLength125", error.ErrorCode);
        }

        [Fact]
        public void DealTypeMaxLength125()
        {
            var entity = CreateEntity(dealType: CreateString(126));

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(TradeEntity.DealType));

            var error = Assert.Single(errors);
            Assert.Equal("DealTypeMaxLength125", error.ErrorCode);
        }

        [Fact]
        public void SourceListIdMaxLength125()
        {
            var entity = CreateEntity(sourceListId: CreateString(126));

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(TradeEntity.SourceListId));

            var error = Assert.Single(errors);
            Assert.Equal("SourceListIdMaxLength125", error.ErrorCode);
        }

        [Fact]
        public void SideMaxLength125()
        {
            var entity = CreateEntity(side: CreateString(126));

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(TradeEntity.Side));

            var error = Assert.Single(errors);
            Assert.Equal("SideMaxLength125", error.ErrorCode);
        }

        private static TestTradeEntityValidatorBase CreateValidator() =>
            new TestTradeEntityValidatorBase();
        private static TradeEntity CreateEntity(
            int id = 1,
            string account = "Test Account",
            string type = "Test Type",
            double buyQuantity = 1,
            double sellQuantity = 1,
            double buyPrice = 1,
            double sellPrice = 1,
            string benchmark = "Test Benchmark",
            DateTimeOffset? tradeDate = null,
            string security = "Test Security",
            string status = "TestStatus",
            string trader = "Test Trader",
            string book = "Test Book",
            string creationName = "Test Creation Name",
            DateTimeOffset? creationDate = null,
            string revisionName = "Test Revision Name",
            DateTimeOffset? revisionDate = null,
            string dealName = "Test Deal Name",
            string dealType = "Test Deal Type",
            string sourceListId = "TestSourceListId",
            string side = "Test Side") =>
            new TradeEntity()
            {
                Id = id,
                Account = account,
                Type = type,
                BuyQuantity = buyQuantity,
                SellQuantity = sellQuantity,
                BuyPrice = buyPrice,
                SellPrice = sellPrice,
                Benchmark = benchmark,
                TradeDate = tradeDate ?? DateTimeOffset.UtcNow,
                Security = security,
                Status = status,
                Trader = trader,
                Book = book,
                CreationName = creationName,
                CreationDate = creationDate ?? DateTimeOffset.UtcNow,
                RevisionName = revisionName,
                RevisionDate = revisionDate ?? DateTimeOffset.UtcNow,
                DealName = dealName,
                DealType = dealType,
                SourceListId = sourceListId,
                Side = side,
            };

        private class TestTradeEntityValidatorBase
            : TradeEntityValidatorBase<TradeEntity>
        { }
    }
}