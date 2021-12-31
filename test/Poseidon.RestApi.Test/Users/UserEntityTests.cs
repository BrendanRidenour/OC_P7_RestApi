using Poseidon.RestApi.Internal;
using System.ComponentModel.DataAnnotations;
using Xunit;
using static Poseidon.RestApi.TestHelpers;

namespace Poseidon.RestApi.Users
{
    public class UserEntityTests
    {
        [Fact]
        public void InheritsEntityBase()
        {
            Assert.True(typeof(EntityBase).IsAssignableFrom(typeof(UserEntity)));
        }

        [Fact]
        public void ImplementsIUserData()
        {
            Assert.True(typeof(IUserData).IsAssignableFrom(typeof(UserEntity)));
        }

        [Fact]
        public void UsernameProperty_HasRequiredAttribute()
        {
            var attribute = GetPropertyAttribute<UserEntity, RequiredAttribute>(
                "Username");

            Assert.NotNull(attribute);
        }

        [Fact]
        public void PasswordProperty_HasRequiredAttribute()
        {
            var attribute = GetPropertyAttribute<UserEntity, RequiredAttribute>(
                "Password");

            Assert.NotNull(attribute);
        }

        [Fact]
        public void FullnameProperty_HasRequiredAttribute()
        {
            var attribute = GetPropertyAttribute<UserEntity, RequiredAttribute>(
                "Fullname");

            Assert.NotNull(attribute);
        }

        [Fact]
        public void RoleProperty_HasRequiredAttribute()
        {
            var attribute = GetPropertyAttribute<UserEntity, RequiredAttribute>(
                "Role");

            Assert.NotNull(attribute);
        }
    }
}