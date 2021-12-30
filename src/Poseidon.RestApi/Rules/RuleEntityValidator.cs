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
                .NotEmpty()
                .WithErrorCode("DescriptionEmpty")
                .MaximumLength(125)
                .WithErrorCode("DescriptionMaxLength125");

            RuleFor(e => e.Json)
                .NotEmpty()
                .WithErrorCode("JsonEmpty")
                .MaximumLength(125)
                .WithErrorCode("JsonMaxLength125");

            RuleFor(e => e.Template)
                .NotEmpty()
                .WithErrorCode("TemplateEmpty")
                .MaximumLength(512)
                .WithErrorCode("TemplateMaxLength512");

            RuleFor(e => e.SqlStr)
                .NotEmpty()
                .WithErrorCode("SqlStrEmpty")
                .MaximumLength(125)
                .WithErrorCode("SqlStrMaxLength125");

            RuleFor(e => e.SqlPart)
                .NotEmpty()
                .WithErrorCode("SqlPartEmpty")
                .MaximumLength(125)
                .WithErrorCode("SqlPartMaxLength125");
        }
    }
}