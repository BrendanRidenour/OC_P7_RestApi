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
        private readonly Claim _jwtMarkerClaim = new Claim(ClaimTypes.AuthenticationMethod, "jwt");

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

        public IUserData? ParseUserData(ClaimsPrincipal? principal)
        {
            var claimsContainJwtMarker = principal?.Claims?.Where(c =>
            {
                return c.Type == this._jwtMarkerClaim.Type &&
                    c.Value == this._jwtMarkerClaim.Value;
            }).Any() ?? false;

            if (!claimsContainJwtMarker)
                return null;

            return ParseClaims(principal!.Claims);
        }

        protected Claim[] CreateClaims(UserEntity user) => new Claim[]
        {
            new Claim(this._jwtMarkerClaim.Type, this._jwtMarkerClaim.Value),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.GivenName, user.Fullname),
            new Claim(ClaimTypes.Role, user.Role),
        };

        protected IUserData ParseClaims(IEnumerable<Claim> claims)
        {
            return new UserData()
            {
                Id = int.Parse(claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value),
                Username = claims.Single(c => c.Type == ClaimTypes.Name).Value,
                Fullname = claims.Single(c => c.Type == ClaimTypes.GivenName).Value,
                Role = claims.Single(c => c.Type == ClaimTypes.Role).Value,
            };
        }
    }
}