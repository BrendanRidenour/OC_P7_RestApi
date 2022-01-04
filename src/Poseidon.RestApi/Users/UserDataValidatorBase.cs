using FluentValidation;

namespace Poseidon.RestApi.Users
{
    /// <summary>
    /// Provides model validation for types implementing <see cref="IUserData" />
    /// </summary>
    public abstract class UserDataValidatorBase<T> : AbstractValidator<T>
        where T : IUserData
    {
        /// <summary>
        /// Configures validation rules for the properties of <see cref="IUserData" />
        /// </summary>
        protected UserDataValidatorBase()
        {
            RuleFor(e => e.Username)
                .NotEmpty()
                .WithErrorCode("UsernameEmpty")
                .MaximumLength(125)
                .WithErrorCode("UsernameMaxLength125");

            RuleFor(e => e.Fullname)
                .NotEmpty()
                .WithErrorCode("FullnameEmpty")
                .MaximumLength(125)
                .WithErrorCode("FullnameMaxLength125");

            RuleFor(e => e.Role)
                .NotEmpty()
                .WithErrorCode("RoleEmpty")
                .MaximumLength(125)
                .WithErrorCode("RoleMaxLength125");
        }
    }
}