using FluentValidation;
using Poseidon.RestApi.Internal;

namespace Poseidon.RestApi.Trades
{
    /// <summary>
    /// Provides model validation for the <see cref="TradeEntity" />
    /// </summary>
    public class TradeEntityValidator : TradeEntityValidatorBase<TradeEntity>
    {
        /// <summary>
        /// Instantiates the validator and configures validation rules
        /// </summary>
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