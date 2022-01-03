using Poseidon.RestApi.Logins;
using Poseidon.RestApi.Users;
using System.Security.Claims;

namespace Poseidon.RestApi.Mocks
{
    public class MockJwtAuthenticationService : IJwtAuthenticationService
    {
        public UserEntity? CreateJwtToken_InputUser;
        public string CreateJwtToken_Result = "jwttoken";
        public string CreateJwtToken(UserEntity user)
        {
            this.CreateJwtToken_InputUser = user;

            return this.CreateJwtToken_Result;
        }

        public ClaimsPrincipal? ParseUserData_InputPrincipal;
        public IUserData? ParseUserData_Result;
        public IUserData? ParseUserData(ClaimsPrincipal? principal)
        {
            this.ParseUserData_InputPrincipal = principal;

            return this.ParseUserData_Result;
        }
    }
}