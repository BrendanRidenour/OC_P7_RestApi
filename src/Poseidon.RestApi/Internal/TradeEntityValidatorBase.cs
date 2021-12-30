using FluentValidation;

namespace Poseidon.RestApi.Internal
{
    public abstract class TradeEntityValidatorBase<T>
        : AbstractValidator<T>
        where T : TradeEntityBase
    {
        protected TradeEntityValidatorBase()
        {
            RuleFor(e => e.Account)
                .NotEmpty()
                .WithErrorCode("AccountEmpty")
                .MaximumLength(30)
                .WithErrorCode("AccountMaxLength30");

            RuleFor(e => e.Type)
                .NotEmpty()
                .WithErrorCode("TypeEmpty")
                .MaximumLength(30)
                .WithErrorCode("TypeMaxLength30");

            RuleFor(e => e.Benchmark)
                .NotEmpty()
                .WithErrorCode("BenchmarkEmpty")
                .MaximumLength(125)
                .WithErrorCode("BenchmarkMaxLength125");

            RuleFor(e => e.Security)
                .NotEmpty()
                .WithErrorCode("SecurityEmpty")
                .MaximumLength(125)
                .WithErrorCode("SecurityMaxLength125");

            RuleFor(e => e.Status)
                .NotEmpty()
                .WithErrorCode("StatusEmpty")
                .MaximumLength(10)
                .WithErrorCode("StatusMaxLength10");

            RuleFor(e => e.Trader)
                .NotEmpty()
                .WithErrorCode("TraderEmpty")
                .MaximumLength(125)
                .WithErrorCode("TraderMaxLength125");

            RuleFor(e => e.Book)
                .NotEmpty()
                .WithErrorCode("BookEmpty")
                .MaximumLength(125)
                .WithErrorCode("BookMaxLength125");

            RuleFor(e => e.CreationName)
                .NotEmpty()
                .WithErrorCode("CreationNameEmpty")
                .MaximumLength(125)
                .WithErrorCode("CreationNameMaxLength125");

            RuleFor(e => e.RevisionName)
                .NotEmpty()
                .WithErrorCode("RevisionNameEmpty")
                .MaximumLength(125)
                .WithErrorCode("RevisionNameMaxLength125");

            RuleFor(e => e.DealName)
                .NotEmpty()
                .WithErrorCode("DealNameEmpty")
                .MaximumLength(125)
                .WithErrorCode("DealNameMaxLength125");

            RuleFor(e => e.DealType)
                .NotEmpty()
                .WithErrorCode("DealTypeEmpty")
                .MaximumLength(125)
                .WithErrorCode("DealTypeMaxLength125");

            RuleFor(e => e.SourceListId)
                .NotEmpty()
                .WithErrorCode("SourceListIdEmpty")
                .MaximumLength(125)
                .WithErrorCode("SourceListIdMaxLength125");

            RuleFor(e => e.Side)
                .NotEmpty()
                .WithErrorCode("SideEmpty")
                .MaximumLength(125)
                .WithErrorCode("SideMaxLength125");
        }
    }
}