using FluentValidation;
using FluentValidation.Results;
using FluentValidation.Validators;

namespace Poseidon.RestApi.Users
{
    public class UserEntityValidator : UserDataValidatorBase<UserEntity>
    {
        public UserEntityValidator()
        {
            RuleFor(e => e.Password)
                .NotEmpty()
                .WithErrorCode("PasswordEmpty")
                .MaximumLength(125)
                .WithErrorCode("PasswordMaxLength125")
                .MinimumLength(8)
                .WithErrorCode("PasswordMinLength8")
                .Custom((value, context) =>
                {
                    var validator = new RegularExpressionValidator<UserEntity>(
                        "[0-9]");

                    if (!validator.IsValid(context, value))
                    {
                        var failure = new ValidationFailure(nameof(UserEntity.Password),
                            "Password must contain at least one number")
                        {
                            ErrorCode = "PasswordAtLeastOneNumber",
                        };

                        context.AddFailure(failure);
                    }
                })
                .Custom((value, context) =>
                {
                    var validator = new RegularExpressionValidator<UserEntity>(
                        "[A-Z]");

                    if (!validator.IsValid(context, value))
                    {
                        var failure = new ValidationFailure(nameof(UserEntity.Password),
                            "Password must contain at least one uppercase letter")
                        {
                            ErrorCode = "PasswordAtLeastOneUppercaseLetter",
                        };

                        context.AddFailure(failure);
                    }
                })
                .Custom((value, context) =>
                {
                    var validator = new RegularExpressionValidator<UserEntity>(
                        "[-+_!@#$%^&*.,?]");

                    if (!validator.IsValid(context, value))
                    {
                        var failure = new ValidationFailure(nameof(UserEntity.Password),
                            "Password must contain at least one symbol")
                        {
                            ErrorCode = "PasswordAtLeastOneSymbol",
                        };

                        context.AddFailure(failure);
                    }
                });
        }
    }
}