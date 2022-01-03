using Poseidon.RestApi.Internal;
using System;
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

        [Theory]
        [InlineData(1, 2)]
        [InlineData(3, 4)]
        public void CopyProperties_WhenCalled_SetsProperties(int idA, int idB)
        {
            var entityA = Entity(idA);
            var entityB = Entity(idB);

            entityA.CopyProperties(entityB);

            Assert.NotEqual(entityB.Id, entityA.Id);
            Assert.Equal(entityB.Username, entityA.Username);
            Assert.Equal(entityB.Password, entityA.Password);
            Assert.Equal(entityB.Fullname, entityA.Fullname);
            Assert.Equal(entityB.Role, entityA.Role);
        }

        private static UserEntity Entity(int id) =>
            new UserEntity()
            {
                Id = id,
                Username = $"username{id}",
                Password = $"password{id}",
                Fullname = $"fullname{id}",
                Role = $"role{id}",
            };
    }
}