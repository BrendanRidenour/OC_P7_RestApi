using FluentValidation;

namespace Poseidon.RestApi.Rules
{
    public class RuleEntityValidator : AbstractValidator<RuleEntity>
    {
        public RuleEntityValidator()
        {
            RuleFor(e => e.Name)
                .NotEmpty()
                .WithErrorCode("NameEmpty")
                .MaximumLength(125)
                .WithErrorCode("NameMaxLength125");

            RuleFor(e => e.Description)
                .MaximumLength(125)
                .WithErrorCode("DescriptionMaxLength125");

            RuleFor(e => e.Json)
                .MaximumLength(125)
                .WithErrorCode("JsonMaxLength125");

            RuleFor(e => e.Template)
                .MaximumLength(512)
                .WithErrorCode("TemplateMaxLength512");

            RuleFor(e => e.SqlStr)
                .MaximumLength(125)
                .WithErrorCode("SqlStrMaxLength125");

            RuleFor(e => e.SqlPart)
                .MaximumLength(125)
                .WithErrorCode("SqlPartMaxLength125");
        }
    }
}