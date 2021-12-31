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
                .MaximumLength(125)
                .WithErrorCode("BenchmarkMaxLength125");

            RuleFor(e => e.Security)
                .MaximumLength(125)
                .WithErrorCode("SecurityMaxLength125");

            RuleFor(e => e.Status)
                .MaximumLength(10)
                .WithErrorCode("StatusMaxLength10");

            RuleFor(e => e.Trader)
                .MaximumLength(125)
                .WithErrorCode("TraderMaxLength125");

            RuleFor(e => e.Book)
                .MaximumLength(125)
                .WithErrorCode("BookMaxLength125");

            RuleFor(e => e.CreationName)
                .MaximumLength(125)
                .WithErrorCode("CreationNameMaxLength125");

            RuleFor(e => e.RevisionName)
                .MaximumLength(125)
                .WithErrorCode("RevisionNameMaxLength125");

            RuleFor(e => e.DealName)
                .MaximumLength(125)
                .WithErrorCode("DealNameMaxLength125");

            RuleFor(e => e.DealType)
                .MaximumLength(125)
                .WithErrorCode("DealTypeMaxLength125");

            RuleFor(e => e.SourceListId)
                .MaximumLength(125)
                .WithErrorCode("SourceListIdMaxLength125");

            RuleFor(e => e.Side)
                .MaximumLength(125)
                .WithErrorCode("SideMaxLength125");
        }
    }
}