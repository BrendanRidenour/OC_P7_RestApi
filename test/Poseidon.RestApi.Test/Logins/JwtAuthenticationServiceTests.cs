using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Internal;
using Microsoft.IdentityModel.Tokens;
using Poseidon.RestApi.Mocks;
using Poseidon.RestApi.Users;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using Xunit;

namespace Poseidon.RestApi.Logins
{
    public class JwtAuthenticationServiceTests
    {
        [Fact]
        public void ImplementsIJwtAuthenticationService()
        {
            Assert.True(typeof(IJwtAuthenticationService)
                .IsAssignableFrom(typeof(JwtAuthenticationService)));
        }

        [Theory]
        [InlineData("keykeykeykeykeykeykey1", "https://1a.example.com", "https://1b.example.com", 1, "Username1", "First1 Last1", "Role1")]
        [InlineData("keykeykeykeykeykeykey2", "https://2a.example.com", "https://2b.example.com", 2, "Username2", "First2 Last2", "Role2")]
        public void CreateJwtToken_WhenCalled_ReturnsJwtToken(string key,
            string issuer, string audience, int id, string username, string fullname,
            string role)
        {
            var config = Config(key, issuer, audience);
            var clock = Clock(DateTimeOffset.UtcNow);
            var service = Service(config, clock);
            var user = User(id, username, "P@ssw0rd", fullname, role);

            var token = service.CreateJwtToken(user);

            var expected = ExpectedToken(config, user, clock);

            Assert.Equal(expected, token);
        }

        [Fact]
        public void ParseUserData_NullPrincipal_ReturnsNull()
        {
            var service = Service();

            var result = service.ParseUserData(principal: null!);

            Assert.Null(result);
        }

        [Fact]
        public void ParseUserData_IdentityNotClaimsIdentity_ReturnsNull()
        {
            var identity = new MockNotClaimsIdentity(isAuthenticated: true);
            var principal = new ClaimsPrincipal(identity);
            var service = Service();

            var result = service.ParseUserData(principal);

            Assert.Null(result);
        }

        [Fact]
        public void ParseUserData_IdentityHasWrongAuthenticationType_ReturnsNull()
        {
            var identity = new ClaimsIdentity(authenticationType: null);
            var principal = new ClaimsPrincipal(identity);
            var service = Service();

            var result = service.ParseUserData(principal);

            Assert.Null(result);
        }

        [Fact]
        public void ParseUserData_IdentityIsNotAuthenticated_ReturnsNull()
        {
            var identity = new MockClaimsIdentity(
                authenticationType: JwtBearerDefaults.AuthenticationScheme,
                isAuthenticated: false);
            var principal = new ClaimsPrincipal(identity);
            var service = Service();

            var result = service.ParseUserData(principal);

            Assert.Null(result);
        }

        [Theory]
        [InlineData(1, "Username1", "P@ssw0rd1", "First1 Last1", "Role1")]
        [InlineData(2, "Username2", "P@ssw0rd2", "First2 Last2", "Role2")]
        public void ParseUserData_WhenCalled_ReturnsUserData(int id, string username,
            string password, string fullname, string role)
        {
            var user = User(id, username, password, fullname, role);
            var principal = Principal(user);
            var service = Service();

            var result = service.ParseUserData(principal);

            Assert.NotNull(result);
            Assert.Equal(id, result!.Id);
            Assert.Equal(username, result.Username);
            Assert.Equal(fullname, result.Fullname);
            Assert.Equal(role, result.Role);
        }

        private static JwtConfiguration Config(string key = "key",
            string issuer = "https://example.com",
            string audience = "https://example.com") =>
            new JwtConfiguration(key, issuer, audience, TimeSpan.FromMinutes(30));
        private static MockSystemClock Clock(DateTimeOffset dateTime) =>
            new MockSystemClock(dateTime);
        private static JwtAuthenticationService Service(
            JwtConfiguration? config = null, ISystemClock? clock = null)
        {
            return new JwtAuthenticationService(
                jwtConfig: config ?? Config(),
                clock: clock ?? Clock(DateTimeOffset.UtcNow));
        }
        private static UserEntity User(int id = 1, string username = "Username",
            string password = "P@ssw0rd", string fullname = "Full Name",
            string role = "Role") =>
            new UserEntity()
            {
                Id = id,
                Username = username,
                Password = password,
                Fullname = fullname,
                Role = role,
            };
        private static string ExpectedToken(JwtConfiguration config,
            UserEntity user, MockSystemClock clock)
        {
            var signingCredentials = new SigningCredentials(
                config.IssuerSigningKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(config.Issuer, config.Audience,
                Claims(user),
                expires: clock.UtcNow.Add(config.ExpiresAfter).DateTime,
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private static Claim[] Claims(UserEntity user) => new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.GivenName, user.Fullname),
            new Claim(ClaimTypes.Role, user.Role),
        };
        private static ClaimsPrincipal Principal(UserEntity user)
        {
            var identity = new ClaimsIdentity(Claims(user),
                JwtBearerDefaults.AuthenticationScheme);

            return new ClaimsPrincipal(identity);
        }
    }
}