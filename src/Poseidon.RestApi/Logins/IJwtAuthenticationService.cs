using Poseidon.RestApi.Users;
using System.Security.Claims;

namespace Poseidon.RestApi.Logins
{
    public interface IJwtAuthenticationService
    {
        string CreateJwtToken(UserEntity user);
        IUserData? ParseUserData(ClaimsPrincipal principal);
    }
}