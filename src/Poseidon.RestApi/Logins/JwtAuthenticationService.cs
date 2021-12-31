using Microsoft.Extensions.Internal;
using Microsoft.IdentityModel.Tokens;
using Poseidon.RestApi.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Poseidon.RestApi.Logins
{
    public class JwtAuthenticationService : IJwtAuthenticationService
    {
        private readonly JwtConfiguration _jwtConfig;
        private readonly ISystemClock _clock;

        public JwtAuthenticationService(JwtConfiguration jwtConfig, ISystemClock clock)
        {
            this._jwtConfig = jwtConfig;
            this._clock = clock;
        }

        public string CreateJwtToken(UserEntity user)
        {
            var securityKey = this._jwtConfig.IssuerSigningKey;
            var signingCredentials = new SigningCredentials(securityKey,
                SecurityAlgorithms.HmacSha256);

            var claims = CreateClaims(user);

            var token = new JwtSecurityToken(
                issuer: this._jwtConfig.Issuer,
                audience: this._jwtConfig.Audience,
                claims: claims,
                expires: this._clock.UtcNow.Add(this._jwtConfig.ExpiresAfter).DateTime,
                signingCredentials: signingCredentials);

            var handler = new JwtSecurityTokenHandler();

            return handler.WriteToken(token);
        }

        public IUserData? ParseUserData(ClaimsPrincipal principal)
        {
            var identity = principal?.Identities?.SingleOrDefault(
                p => p.AuthenticationType == this._jwtConfig.AuthenticationScheme);

            if (identity is null || !identity.IsAuthenticated)
                return null;

            return ParseClaims(identity);
        }

        protected Claim[] CreateClaims(UserEntity user) => new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.GivenName, user.Fullname),
            new Claim(ClaimTypes.Role, user.Role),
        };

        protected IUserData ParseClaims(ClaimsIdentity identity)
        {
            var claims = identity.Claims;

            return new UserEntity()
            {
                Id = int.Parse(claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value),
                Username = claims.Single(c => c.Type == ClaimTypes.Name).Value,
                Fullname = claims.Single(c => c.Type == ClaimTypes.GivenName).Value,
                Role = claims.Single(c => c.Type == ClaimTypes.Role).Value,
            };
        }
    }
}