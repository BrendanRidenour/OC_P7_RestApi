using FluentValidation;

namespace Poseidon.RestApi.Logins
{
    public class LoginCredentialsValidator : AbstractValidator<LoginCredentials>
    {
        public LoginCredentialsValidator()
        {
            RuleFor(l => l.Username)
                .NotEmpty()
                .WithErrorCode("UsernameEmpty")
                .MaximumLength(125)
                .WithErrorCode("UsernameMaxLength125");

            RuleFor(l => l.Password)
                .NotEmpty()
                .WithErrorCode("PasswordEmpty")
                .MaximumLength(125)
                .WithErrorCode("PasswordMaxLength125");
        }
    }
}