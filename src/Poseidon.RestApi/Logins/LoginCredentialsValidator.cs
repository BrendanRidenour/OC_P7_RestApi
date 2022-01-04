using FluentValidation;

namespace Poseidon.RestApi.Logins
{
    /// <summary>
    /// Provides model validation for the <see cref="LoginCredentials" />
    /// </summary>
    public class LoginCredentialsValidator : AbstractValidator<LoginCredentials>
    {
        /// <summary>
        /// Instantiates the validator and configures validation rules
        /// </summary>
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