using FluentValidation;
using System.Linq;
using Xunit;
using static Poseidon.RestApi.TestHelpers;

namespace Poseidon.RestApi.Logins
{
    public class LoginCredentialsValidatorTests
    {
        [Fact]
        public void InheritsAbstractValidator()
        {
            Assert.True(typeof(AbstractValidator<LoginCredentials>)
                .IsAssignableFrom(typeof(LoginCredentialsValidator)));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void UsernameEmpty(string? value)
        {
            var entity = CreateEntity(username: value!);

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(LoginCredentials.Username));

            var error = Assert.Single(errors);
            Assert.Equal("UsernameEmpty", error.ErrorCode);
        }

        [Fact]
        public void UsernameMaxLength125()
        {
            var entity = CreateEntity(username: CreateString(126));

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(LoginCredentials.Username));

            var error = Assert.Single(errors);
            Assert.Equal("UsernameMaxLength125", error.ErrorCode);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void PasswordEmpty(string? value)
        {
            var entity = CreateEntity(password: value!);

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(LoginCredentials.Password));

            Assert.Single(errors.Where(e => e.ErrorCode.Equals("PasswordEmpty")));
        }

        [Fact]
        public void PasswordMaxLength125()
        {
            var entity = CreateEntity(password: CreateString(126) + "A1!");

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(LoginCredentials.Password));

            var error = Assert.Single(errors);
            Assert.Equal("PasswordMaxLength125", error.ErrorCode);
        }

        private static LoginCredentialsValidator CreateValidator() =>
            new LoginCredentialsValidator();
        private static LoginCredentials CreateEntity(
            string username = "Username",
            string password = "P@ssw0rd") =>
            new LoginCredentials()
            {
                Username = username,
                Password = password,
            };
    }
}