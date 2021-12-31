using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static Poseidon.RestApi.TestHelpers;

namespace Poseidon.RestApi.Logins
{
    public class LoginCredentialsTests
    {
        [Fact]
        public void UsernameProperty_HasRequiredAttribute()
        {
            var attribute = GetPropertyAttribute<LoginCredentials, RequiredAttribute>(
                "Username");

            Assert.NotNull(attribute);
        }

        [Fact]
        public void PasswordProperty_HasRequiredAttribute()
        {
            var attribute = GetPropertyAttribute<LoginCredentials, RequiredAttribute>(
                "Password");

            Assert.NotNull(attribute);
        }
    }
}