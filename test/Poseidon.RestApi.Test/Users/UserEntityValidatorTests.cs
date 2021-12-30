using FluentValidation;
using System.Linq;
using System.Text;
using Xunit;

namespace Poseidon.RestApi.Users
{
    public class UserEntityValidatorTests
    {
        [Fact]
        public void InheritsAbstractValidator()
        {
            Assert.True(typeof(AbstractValidator<UserEntity>)
                .IsAssignableFrom(typeof(UserEntityValidator)));
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
                .Where(e => e.PropertyName == nameof(UserEntity.Username));

            var error = Assert.Single(errors);
            Assert.Equal("UsernameEmpty", error.ErrorCode);
        }

        [Fact]
        public void UsernameMaxLength125()
        {
            var entity = CreateEntity(username: CreateString(126));

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(UserEntity.Username));

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
                .Where(e => e.PropertyName == nameof(UserEntity.Password));

            Assert.Single(errors.Where(e => e.ErrorCode.Equals("PasswordEmpty")));
        }

        [Fact]
        public void PasswordMaxLength125()
        {
            var entity = CreateEntity(password: CreateString(126) + "A1!");

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(UserEntity.Password));

            var error = Assert.Single(errors);
            Assert.Equal("PasswordMaxLength125", error.ErrorCode);
        }

        [Fact]
        public void PasswordMinLength8()
        {
            var entity = CreateEntity(password: CreateString(4) + "A1!");

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(UserEntity.Password));

            var error = Assert.Single(errors);
            Assert.Equal("PasswordMinLength8", error.ErrorCode);
        }

        [Fact]
        public void PasswordAtLeastOneNumber()
        {
            var entity = CreateEntity(password: "No_number_so_will_fail");

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(UserEntity.Password));

            var error = Assert.Single(errors);
            Assert.Equal("PasswordAtLeastOneNumber", error.ErrorCode);
            Assert.Equal("Password must contain at least one number",
                error.ErrorMessage);
        }

        [Fact]
        public void PasswordAtLeastOneUppercaseLetter()
        {
            var entity = CreateEntity(password: "nouppercasewillfail1!");

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(UserEntity.Password));

            var error = Assert.Single(errors);
            Assert.Equal("PasswordAtLeastOneUppercaseLetter", error.ErrorCode);
            Assert.Equal("Password must contain at least one uppercase letter",
                error.ErrorMessage);
        }

        [Fact]
        public void PasswordAtLeastOneSymbol()
        {
            var entity = CreateEntity(password: "Nosymbolwillfail1");

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(UserEntity.Password));

            var error = Assert.Single(errors);
            Assert.Equal("PasswordAtLeastOneSymbol", error.ErrorCode);
            Assert.Equal("Password must contain at least one symbol",
                error.ErrorMessage);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void FullnameEmpty(string? value)
        {
            var entity = CreateEntity(fullname: value!);

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(UserEntity.Fullname));

            var error = Assert.Single(errors);
            Assert.Equal("FullnameEmpty", error.ErrorCode);
        }

        [Fact]
        public void FullnameMaxLength125()
        {
            var entity = CreateEntity(fullname: CreateString(126));

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(UserEntity.Fullname));

            var error = Assert.Single(errors);
            Assert.Equal("FullnameMaxLength125", error.ErrorCode);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void RoleEmpty(string? value)
        {
            var entity = CreateEntity(role: value!);

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(UserEntity.Role));

            var error = Assert.Single(errors);
            Assert.Equal("RoleEmpty", error.ErrorCode);
        }

        [Fact]
        public void RoleMaxLength125()
        {
            var entity = CreateEntity(role: CreateString(126));

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(UserEntity.Role));

            var error = Assert.Single(errors);
            Assert.Equal("RoleMaxLength125", error.ErrorCode);
        }

        private static UserEntityValidator CreateValidator() =>
            new UserEntityValidator();
        private static UserEntity CreateEntity(
            int id = 1,
            string username = "TestUsername",
            string password = "TestPassword1!",
            string fullname = "Test Full Name",
            string role = "Test Role") =>
            new UserEntity()
            {
                Id = id,
                Username = username,
                Password = password,
                Fullname = fullname,
                Role = role,
            };
        private static string CreateString(int length)
        {
            var builder = new StringBuilder();

            for (var i = 0; i < length; i++)
                builder.Append('0');

            return builder.ToString();
        }
    }
}