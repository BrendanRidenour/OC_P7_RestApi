using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using Xunit;

namespace Poseidon.RestApi.Logins
{
    public class JwtConfigurationTests
    {
        [Fact]
        public void Constructor_WhenCalled_SetsAuthenticationSchemeToJwtBearerDefaults()
        {
            var domain = "https://example.com";

            var config = new JwtConfiguration("key", issuer: domain, audience: domain,
                expiresAfter: TimeSpan.FromMinutes(30));

            Assert.Equal(JwtBearerDefaults.AuthenticationScheme,
                config.AuthenticationScheme);
        }

        [Theory]
        [InlineData("key1")]
        [InlineData("key2")]
        public void Constructor_WhenCalled_SetsIssuerSigningKey(string key)
        {
            var domain = "https://example.com";

            var config = new JwtConfiguration(key, issuer: domain, audience: domain,
                expiresAfter: TimeSpan.FromMinutes(30));

            var expected = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            Assert.Equal(expected.Key, config.IssuerSigningKey.Key);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void Constructor_WhenCalled_SetsExpiresAfterToMinutes(
            int expiresAfterMinutes)
        {
            var domain = "https://example.com";

            var config = new JwtConfiguration("key", issuer: domain, audience: domain,
                expiresAfter: TimeSpan.FromMinutes(expiresAfterMinutes));

            Assert.Equal(TimeSpan.FromMinutes(expiresAfterMinutes), config.ExpiresAfter);
        }
    }
}