using FluentValidation;
using System;
using System.Linq;
using System.Text;
using Xunit;

namespace Poseidon.RestApi.Trades
{
    public class TradeEntityValidatorTests
    {
        [Fact]
        public void InheritsAbstractValidator()
        {
            Assert.True(typeof(AbstractValidator<TradeEntity>)
                .IsAssignableFrom(typeof(TradeEntityValidator)));
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

        [Theory]
        [InlineData(-0.1)]
        [InlineData(-1.0)]
        public void BuyQuantityLessThanZero(double value)
        {
            var entity = CreateEntity(buyQuantity: value);

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(TradeEntity.BuyQuantity));

            var error = Assert.Single(errors);
            Assert.Equal("BuyQuantityLessThanZero", error.ErrorCode);
        }

        [Theory]
        [InlineData(-0.1)]
        [InlineData(-1.0)]
        public void SellQuantityLessThanZero(double value)
        {
            var entity = CreateEntity(sellQuantity: value);

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(TradeEntity.SellQuantity));

            var error = Assert.Single(errors);
            Assert.Equal("SellQuantityLessThanZero", error.ErrorCode);
        }

        [Theory]
        [InlineData(-0.1)]
        [InlineData(-1.0)]
        public void BuyPriceLessThanZero(double value)
        {
            var entity = CreateEntity(buyPrice: value);

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(TradeEntity.BuyPrice));

            var error = Assert.Single(errors);
            Assert.Equal("BuyPriceLessThanZero", error.ErrorCode);
        }

        [Theory]
        [InlineData(-0.1)]
        [InlineData(-1.0)]
        public void SellPriceLessThanZero(double value)
        {
            var entity = CreateEntity(sellPrice: value);

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(TradeEntity.SellPrice));

            var error = Assert.Single(errors);
            Assert.Equal("SellPriceLessThanZero", error.ErrorCode);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void BenchmarkEmpty(string? value)
        {
            var entity = CreateEntity(benchmark: value!);

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(TradeEntity.Benchmark));

            var error = Assert.Single(errors);
            Assert.Equal("BenchmarkEmpty", error.ErrorCode);
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

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void SecurityEmpty(string? value)
        {
            var entity = CreateEntity(security: value!);

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(TradeEntity.Security));

            var error = Assert.Single(errors);
            Assert.Equal("SecurityEmpty", error.ErrorCode);
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

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void StatusEmpty(string? value)
        {
            var entity = CreateEntity(status: value!);

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(TradeEntity.Status));

            var error = Assert.Single(errors);
            Assert.Equal("StatusEmpty", error.ErrorCode);
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

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void TraderEmpty(string? value)
        {
            var entity = CreateEntity(trader: value!);

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(TradeEntity.Trader));

            var error = Assert.Single(errors);
            Assert.Equal("TraderEmpty", error.ErrorCode);
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

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void BookEmpty(string? value)
        {
            var entity = CreateEntity(book: value!);

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(TradeEntity.Book));

            var error = Assert.Single(errors);
            Assert.Equal("BookEmpty", error.ErrorCode);
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

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void CreationNameEmpty(string? value)
        {
            var entity = CreateEntity(creationName: value!);

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(TradeEntity.CreationName));

            var error = Assert.Single(errors);
            Assert.Equal("CreationNameEmpty", error.ErrorCode);
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

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void RevisionNameEmpty(string? value)
        {
            var entity = CreateEntity(revisionName: value!);

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(TradeEntity.RevisionName));

            var error = Assert.Single(errors);
            Assert.Equal("RevisionNameEmpty", error.ErrorCode);
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

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void DealNameEmpty(string? value)
        {
            var entity = CreateEntity(dealName: value!);

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(TradeEntity.DealName));

            var error = Assert.Single(errors);
            Assert.Equal("DealNameEmpty", error.ErrorCode);
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

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void DealTypeEmpty(string? value)
        {
            var entity = CreateEntity(dealType: value!);

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(TradeEntity.DealType));

            var error = Assert.Single(errors);
            Assert.Equal("DealTypeEmpty", error.ErrorCode);
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

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void SourceListIdEmpty(string? value)
        {
            var entity = CreateEntity(sourceListId: value!);

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(TradeEntity.SourceListId));

            var error = Assert.Single(errors);
            Assert.Equal("SourceListIdEmpty", error.ErrorCode);
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

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void SideEmpty(string? value)
        {
            var entity = CreateEntity(side: value!);

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(TradeEntity.Side));

            var error = Assert.Single(errors);
            Assert.Equal("SideEmpty", error.ErrorCode);
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

        private static TradeEntityValidator CreateValidator() =>
            new TradeEntityValidator();
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
        private static string CreateString(int length)
        {
            var builder = new StringBuilder();

            for (var i = 0; i < length; i++)
                builder.Append('0');

            return builder.ToString();
        }
    }
}