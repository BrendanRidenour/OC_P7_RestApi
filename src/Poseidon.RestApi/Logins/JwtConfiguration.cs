using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Poseidon.RestApi.Logins
{
    /// <summary>
    /// A class to configure JWT authentication
    /// </summary>
    public class JwtConfiguration
    {
        public string AuthenticationScheme { get; } = JwtBearerDefaults.AuthenticationScheme;
        public string Key { get; }
        public string Issuer { get; }
        public SymmetricSecurityKey IssuerSigningKey { get; }
        public string Audience { get; }
        public TimeSpan ExpiresAfter { get; }

        public JwtConfiguration(string key, string issuer, string audience,
            TimeSpan expiresAfter)
        {
            Key = key;
            Issuer = issuer;
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
            Audience = audience;
            ExpiresAfter = expiresAfter;
        }
    }
}