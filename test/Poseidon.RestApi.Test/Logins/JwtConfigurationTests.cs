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

            var config = new JwtConfiguration("key", issuer: domain, audience: domain);

            Assert.Equal(JwtBearerDefaults.AuthenticationScheme,
                config.AuthenticationScheme);
        }

        [Theory]
        [InlineData("key1")]
        [InlineData("key2")]
        public void Constructor_WhenCalled_SetsIssuerSigningKey(string key)
        {
            var domain = "https://example.com";

            var config = new JwtConfiguration(key, issuer: domain, audience: domain);

            var expected = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            Assert.Equal(expected.Key, config.IssuerSigningKey.Key);
        }

        [Fact]
        public void Constructor_WhenCalled_SetsExpiresAfterTo30Minutes()
        {
            var domain = "https://example.com";

            var config = new JwtConfiguration("key", issuer: domain, audience: domain);

            Assert.Equal(TimeSpan.FromMinutes(30), config.ExpiresAfter);
        }
    }
}