using FluentValidation;
using System.Linq;
using Xunit;
using static Poseidon.RestApi.TestHelpers;

namespace Poseidon.RestApi.Users
{
    public class UserDataValidatorTests
    {
        [Fact]
        public void InheritsUserDataValidatorBase()
        {
            Assert.True(typeof(UserDataValidatorBase<UserData>)
                .IsAssignableFrom(typeof(UserDataValidator)));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void UsernameEmpty(string? value)
        {
            var data = CreateData(username: value!);

            var result = CreateValidator().Validate(data);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(UserEntity.Username));

            var error = Assert.Single(errors);
            Assert.Equal("UsernameEmpty", error.ErrorCode);
        }

        [Fact]
        public void UsernameMaxLength125()
        {
            var data = CreateData(username: CreateString(126));

            var result = CreateValidator().Validate(data);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(UserEntity.Username));

            var error = Assert.Single(errors);
            Assert.Equal("UsernameMaxLength125", error.ErrorCode);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void FullnameEmpty(string? value)
        {
            var data = CreateData(fullname: value!);

            var result = CreateValidator().Validate(data);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(UserEntity.Fullname));

            var error = Assert.Single(errors);
            Assert.Equal("FullnameEmpty", error.ErrorCode);
        }

        [Fact]
        public void FullnameMaxLength125()
        {
            var data = CreateData(fullname: CreateString(126));

            var result = CreateValidator().Validate(data);

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
            var data = CreateData(role: value!);

            var result = CreateValidator().Validate(data);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(UserEntity.Role));

            var error = Assert.Single(errors);
            Assert.Equal("RoleEmpty", error.ErrorCode);
        }

        [Fact]
        public void RoleMaxLength125()
        {
            var data = CreateData(role: CreateString(126));

            var result = CreateValidator().Validate(data);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(UserEntity.Role));

            var error = Assert.Single(errors);
            Assert.Equal("RoleMaxLength125", error.ErrorCode);
        }

        private static UserDataValidator CreateValidator() => new UserDataValidator();
        private static UserData CreateData(
            int id = 1,
            string username = "TestUsername",
            string fullname = "Test Full Name",
            string role = "Test Role") =>
            new UserData()
            {
                Id = id,
                Username = username,
                Fullname = fullname,
                Role = role,
            };
    }
}