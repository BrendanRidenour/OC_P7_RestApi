using FluentValidation;
using Poseidon.RestApi.Internal;

namespace Poseidon.RestApi.Bids
{
    public class BidEntityValidator : TradeEntityValidatorBase<BidEntity>
    {
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