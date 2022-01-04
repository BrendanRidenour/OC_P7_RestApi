using FluentValidation;
using Poseidon.RestApi.Internal;

namespace Poseidon.RestApi.Bids
{
    /// <summary>
    /// Provides model validation for the <see cref="BidEntity" />
    /// </summary>
    public class BidEntityValidator : TradeEntityValidatorBase<BidEntity>
    {
        /// <summary>
        /// Instantiates the validator and configures validation rules
        /// </summary>
        public BidEntityValidator()
        {
            RuleFor(e => e.BidQuantity)
                .GreaterThanOrEqualTo(0)
                .WithErrorCode("BidQuantityLessThanZero");

            RuleFor(e => e.AskQuantity)
                .GreaterThanOrEqualTo(0)
                .WithErrorCode("AskQuantityLessThanZero");

            RuleFor(e => e.Bid)
                .GreaterThanOrEqualTo(0)
                .WithErrorCode("BidLessThanZero");

            RuleFor(e => e.Ask)
                .GreaterThanOrEqualTo(0)
                .WithErrorCode("AskLessThanZero");

            RuleFor(e => e.Commentary)
                .MaximumLength(125)
                .WithErrorCode("CommentaryMaxLength125");
        }
    }
}