using FluentValidation;
using System.Linq;
using System.Text;
using Xunit;

namespace Poseidon.RestApi.Ratings
{
    public class RatingEntityValidatorTests
    {
        [Fact]
        public void InheritsAbstractValidator()
        {
            Assert.True(typeof(AbstractValidator<RatingEntity>)
                .IsAssignableFrom(typeof(RatingEntityValidator)));
        }

        [Fact]
        public void MoodysRatingMaxLength125()
        {
            var entity = CreateEntity(moodysRating: CreateString(126));

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(RatingEntity.MoodysRating));

            var error = Assert.Single(errors);
            Assert.Equal("MoodysRatingMaxLength125", error.ErrorCode);
        }

        [Fact]
        public void SandPRatingMaxLength125()
        {
            var entity = CreateEntity(sandPRating: CreateString(126));

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(RatingEntity.SandPRating));

            var error = Assert.Single(errors);
            Assert.Equal("SandPRatingMaxLength125", error.ErrorCode);
        }

        [Fact]
        public void FitchRatingMaxLength125()
        {
            var entity = CreateEntity(fitchRating: CreateString(126));

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(RatingEntity.FitchRating));

            var error = Assert.Single(errors);
            Assert.Equal("FitchRatingMaxLength125", error.ErrorCode);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void OrderNumberLessThanOne(int value)
        {
            var entity = CreateEntity(orderNumber: value);

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(RatingEntity.OrderNumber));

            var error = Assert.Single(errors);
            Assert.Equal("OrderNumberLessThanOne", error.ErrorCode);
        }

        private static RatingEntityValidator CreateValidator() =>
            new RatingEntityValidator();
        private static RatingEntity CreateEntity(
            int id = 1,
            string moodysRating = "Test Moodys",
            string sandPRating = "Test SandP",
            string fitchRating = "Test Fitch",
            int orderNumber = 1) =>
            new RatingEntity()
            {
                Id = id,
                MoodysRating = moodysRating,
                SandPRating = sandPRating,
                FitchRating = fitchRating,
                OrderNumber = orderNumber,
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