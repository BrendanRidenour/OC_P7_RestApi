using FluentValidation;

namespace Poseidon.RestApi.CurvePoints
{
    /// <summary>
    /// Provides model validation for the <see cref="CurvePointEntity" />
    /// </summary>
    public class CurvePointEntityValidator : AbstractValidator<CurvePointEntity>
    {
        /// <summary>
        /// Instantiates the validator and configures validation rules
        /// </summary>
        public CurvePointEntityValidator()
        {
            RuleFor(e => e.CurveId)
                .GreaterThanOrEqualTo(1)
                .WithErrorCode("CurveIdLessThanOne");

            RuleFor(e => e.Term)
                .GreaterThanOrEqualTo(0)
                .WithErrorCode("TermLessThanZero");

            RuleFor(e => e.Value)
                .GreaterThanOrEqualTo(0)
                .WithErrorCode("ValueLessThanZero");
        }
    }
}