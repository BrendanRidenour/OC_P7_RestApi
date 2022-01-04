using FluentValidation;

namespace Poseidon.RestApi.Ratings
{
    /// <summary>
    /// Provides model validation for the <see cref="RatingEntity" />
    /// </summary>
    public class RatingEntityValidator : AbstractValidator<RatingEntity>
    {
        /// <summary>
        /// Instantiates the validator and configures validation rules
        /// </summary>
        public RatingEntityValidator()
        {
            RuleFor(e => e.MoodysRating)
                .MaximumLength(125)
                .WithErrorCode("MoodysRatingMaxLength125");

            RuleFor(e => e.SandPRating)
                .MaximumLength(125)
                .WithErrorCode("SandPRatingMaxLength125");

            RuleFor(e => e.FitchRating)
                .MaximumLength(125)
                .WithErrorCode("FitchRatingMaxLength125");

            RuleFor(e => e.OrderNumber)
                .GreaterThanOrEqualTo(1)
                .WithErrorCode("OrderNumberLessThanOne");
        }
    }
}