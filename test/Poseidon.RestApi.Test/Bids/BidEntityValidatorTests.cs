using FluentValidation;
using System;
using System.Linq;
using System.Text;
using Xunit;

namespace Poseidon.RestApi.Bids
{
    public class BidEntityValidatorTests
    {
        [Fact]
        public void InheritsAbstractValidator()
        {
            Assert.True(typeof(AbstractValidator<BidEntity>)
                .IsAssignableFrom(typeof(BidEntityValidator)));
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
                .Where(e => e.PropertyName == nameof(BidEntity.Account));

            var error = Assert.Single(errors);
            Assert.Equal("AccountEmpty", error.ErrorCode);
        }

        [Fact]
        public void AccountMaxLength30()
        {
            var entity = CreateEntity(account: CreateString(31));

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(BidEntity.Account));

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
                .Where(e => e.PropertyName == nameof(BidEntity.Type));

            var error = Assert.Single(errors);
            Assert.Equal("TypeEmpty", error.ErrorCode);
        }

        [Fact]
        public void TypeMaxLength30()
        {
            var entity = CreateEntity(type: CreateString(31));

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(BidEntity.Type));

            var error = Assert.Single(errors);
            Assert.Equal("TypeMaxLength30", error.ErrorCode);
        }

        [Theory]
        [InlineData(-0.1)]
        [InlineData(-1.0)]
        public void BidQuantityLessThanZero(double value)
        {
            var entity = CreateEntity(bidQuantity: value);

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(BidEntity.BidQuantity));

            var error = Assert.Single(errors);
            Assert.Equal("BidQuantityLessThanZero", error.ErrorCode);
        }

        [Theory]
        [InlineData(-0.1)]
        [InlineData(-1.0)]
        public void AskQuantityLessThanZero(double value)
        {
            var entity = CreateEntity(askQuantity: value);

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(BidEntity.AskQuantity));

            var error = Assert.Single(errors);
            Assert.Equal("AskQuantityLessThanZero", error.ErrorCode);
        }

        [Theory]
        [InlineData(-0.1)]
        [InlineData(-1.0)]
        public void BidLessThanZero(double value)
        {
            var entity = CreateEntity(bid: value);

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(BidEntity.Bid));

            var error = Assert.Single(errors);
            Assert.Equal("BidLessThanZero", error.ErrorCode);
        }

        [Theory]
        [InlineData(-0.1)]
        [InlineData(-1.0)]
        public void AskLessThanZero(double value)
        {
            var entity = CreateEntity(ask: value);

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(BidEntity.Ask));

            var error = Assert.Single(errors);
            Assert.Equal("AskLessThanZero", error.ErrorCode);
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
                .Where(e => e.PropertyName == nameof(BidEntity.Benchmark));

            var error = Assert.Single(errors);
            Assert.Equal("BenchmarkEmpty", error.ErrorCode);
        }

        [Fact]
        public void BenchmarkMaxLength125()
        {
            var entity = CreateEntity(benchmark: CreateString(126));

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(BidEntity.Benchmark));

            var error = Assert.Single(errors);
            Assert.Equal("BenchmarkMaxLength125", error.ErrorCode);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void CommentaryEmpty(string? value)
        {
            var entity = CreateEntity(commentary: value!);

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(BidEntity.Commentary));

            var error = Assert.Single(errors);
            Assert.Equal("CommentaryEmpty", error.ErrorCode);
        }

        [Fact]
        public void CommentaryMaxLength125()
        {
            var entity = CreateEntity(commentary: CreateString(126));

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(BidEntity.Commentary));

            var error = Assert.Single(errors);
            Assert.Equal("CommentaryMaxLength125", error.ErrorCode);
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
                .Where(e => e.PropertyName == nameof(BidEntity.Security));

            var error = Assert.Single(errors);
            Assert.Equal("SecurityEmpty", error.ErrorCode);
        }

        [Fact]
        public void SecurityMaxLength125()
        {
            var entity = CreateEntity(security: CreateString(126));

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(BidEntity.Security));

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
                .Where(e => e.PropertyName == nameof(BidEntity.Status));

            var error = Assert.Single(errors);
            Assert.Equal("StatusEmpty", error.ErrorCode);
        }

        [Fact]
        public void StatusMaxLength10()
        {
            var entity = CreateEntity(status: CreateString(11));

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(BidEntity.Status));

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
                .Where(e => e.PropertyName == nameof(BidEntity.Trader));

            var error = Assert.Single(errors);
            Assert.Equal("TraderEmpty", error.ErrorCode);
        }

        [Fact]
        public void TraderMaxLength125()
        {
            var entity = CreateEntity(trader: CreateString(126));

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(BidEntity.Trader));

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
                .Where(e => e.PropertyName == nameof(BidEntity.Book));

            var error = Assert.Single(errors);
            Assert.Equal("BookEmpty", error.ErrorCode);
        }

        [Fact]
        public void BookMaxLength125()
        {
            var entity = CreateEntity(book: CreateString(126));

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(BidEntity.Book));

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
                .Where(e => e.PropertyName == nameof(BidEntity.CreationName));

            var error = Assert.Single(errors);
            Assert.Equal("CreationNameEmpty", error.ErrorCode);
        }

        [Fact]
        public void CreationNameMaxLength125()
        {
            var entity = CreateEntity(creationName: CreateString(126));

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(BidEntity.CreationName));

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
                .Where(e => e.PropertyName == nameof(BidEntity.RevisionName));

            var error = Assert.Single(errors);
            Assert.Equal("RevisionNameEmpty", error.ErrorCode);
        }

        [Fact]
        public void RevisionNameMaxLength125()
        {
            var entity = CreateEntity(revisionName: CreateString(126));

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(BidEntity.RevisionName));

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
                .Where(e => e.PropertyName == nameof(BidEntity.DealName));

            var error = Assert.Single(errors);
            Assert.Equal("DealNameEmpty", error.ErrorCode);
        }

        [Fact]
        public void DealNameMaxLength125()
        {
            var entity = CreateEntity(dealName: CreateString(126));

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(BidEntity.DealName));

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
                .Where(e => e.PropertyName == nameof(BidEntity.DealType));

            var error = Assert.Single(errors);
            Assert.Equal("DealTypeEmpty", error.ErrorCode);
        }

        [Fact]
        public void DealTypeMaxLength125()
        {
            var entity = CreateEntity(dealType: CreateString(126));

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(BidEntity.DealType));

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
                .Where(e => e.PropertyName == nameof(BidEntity.SourceListId));

            var error = Assert.Single(errors);
            Assert.Equal("SourceListIdEmpty", error.ErrorCode);
        }

        [Fact]
        public void SourceListIdMaxLength125()
        {
            var entity = CreateEntity(sourceListId: CreateString(126));

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(BidEntity.SourceListId));

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
                .Where(e => e.PropertyName == nameof(BidEntity.Side));

            var error = Assert.Single(errors);
            Assert.Equal("SideEmpty", error.ErrorCode);
        }

        [Fact]
        public void SideMaxLength125()
        {
            var entity = CreateEntity(side: CreateString(126));

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(BidEntity.Side));

            var error = Assert.Single(errors);
            Assert.Equal("SideMaxLength125", error.ErrorCode);
        }

        private static BidEntityValidator CreateValidator() =>
            new BidEntityValidator();
        private static BidEntity CreateEntity(
            int id = 1,
            string account = "Test Account",
            string type = "Test Type",
            double bidQuantity = 1,
            double askQuantity = 1,
            double bid = 1,
            double ask = 1,
            string benchmark = "Test Benchmark",
            DateTimeOffset? bidListDate = null,
            string commentary = "Test Commentary",
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
            new BidEntity()
            {
                Id = id,
                Account = account,
                Type = type,
                BidQuantity = bidQuantity,
                AskQuantity = askQuantity,
                Bid = bid,
                Ask = ask,
                Benchmark = benchmark,
                BidListDate = bidListDate ?? DateTimeOffset.UtcNow,
                Commentary = commentary,
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