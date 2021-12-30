using FluentValidation;

namespace Poseidon.RestApi.CurvePoints
{
    public class CurvePointEntityValidator : AbstractValidator<CurvePointEntity>
    {
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