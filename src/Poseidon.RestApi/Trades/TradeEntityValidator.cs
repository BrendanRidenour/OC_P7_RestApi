using FluentValidation;
using Poseidon.RestApi.Internal;

namespace Poseidon.RestApi.Trades
{
    public class TradeEntityValidator : TradeEntityValidatorBase<TradeEntity>
    {
        public TradeEntityValidator()
        {
            RuleFor(e => e.BuyQuantity)
                .GreaterThanOrEqualTo(0)
                .WithErrorCode("BuyQuantityLessThanZero");

            RuleFor(e => e.SellQuantity)
                .GreaterThanOrEqualTo(0)
                .WithErrorCode("SellQuantityLessThanZero");

            RuleFor(e => e.BuyPrice)
                .GreaterThanOrEqualTo(0)
                .WithErrorCode("BuyPriceLessThanZero");

            RuleFor(e => e.SellPrice)
                .GreaterThanOrEqualTo(0)
                .WithErrorCode("SellPriceLessThanZero");
        }
    }
}