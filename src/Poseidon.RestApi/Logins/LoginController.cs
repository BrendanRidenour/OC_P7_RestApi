using Microsoft.AspNetCore.Mvc;
using Poseidon.RestApi.Data;
using Poseidon.RestApi.Users;

namespace Poseidon.RestApi.Logins
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IReadOperation<Username, UserEntity> _userStore;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtAuthenticationService _jwtAuthenticationService;

        public LoginController(IReadOperation<Username, UserEntity> userStore,
            IPasswordHasher passwordHasher,
            IJwtAuthenticationService jwtAuthenticationService)
        {
            this._userStore = userStore;
            this._passwordHasher = passwordHasher;
            this._jwtAuthenticationService = jwtAuthenticationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateJwtToken(
            [FromBody] LoginCredentials credentials)
        {
            var user = await this._userStore.Read(new Username(credentials.Username!));

            if (user is null)
                return Unauthorized();

            if (!this._passwordHasher.Validate(user.Password, credentials.Password!))
                return Unauthorized();

            var token = this._jwtAuthenticationService.CreateJwtToken(user);

            return Ok(new { token = token });
        }
    }
}