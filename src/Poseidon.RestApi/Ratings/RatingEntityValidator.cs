using FluentValidation;

namespace Poseidon.RestApi.Ratings
{
    public class RatingEntityValidator : AbstractValidator<RatingEntity>
    {
        public RatingEntityValidator()
        {
            RuleFor(e => e.MoodysRating)
                .NotEmpty()
                .WithErrorCode("MoodysRatingEmpty")
                .MaximumLength(125)
                .WithErrorCode("MoodysRatingMaxLength125");

            RuleFor(e => e.SandPRating)
                .NotEmpty()
                .WithErrorCode("SandPRatingEmpty")
                .MaximumLength(125)
                .WithErrorCode("SandPRatingMaxLength125");

            RuleFor(e => e.FitchRating)
                .NotEmpty()
                .WithErrorCode("FitchRatingEmpty")
                .MaximumLength(125)
                .WithErrorCode("FitchRatingMaxLength125");

            RuleFor(e => e.OrderNumber)
                .GreaterThanOrEqualTo(1)
                .WithErrorCode("OrderNumberLessThanOne");
        }
    }
}