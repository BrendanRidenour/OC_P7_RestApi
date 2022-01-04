using Microsoft.AspNetCore.Mvc;
using Poseidon.RestApi.Data;
using Poseidon.RestApi.Users;

namespace Poseidon.RestApi.Logins
{
    /// <summary>
    /// An API controller for performing login attempts
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IReadOperation<Username, UserEntity> _userStore;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtAuthenticationService _jwtAuthenticationService;

        /// <summary>
        /// Instantiates the class
        /// </summary>
        /// <param name="userStore">The data store of users</param>
        /// <param name="passwordHasher">The password hasher to test login passwords</param>
        /// <param name="jwtAuthenticationService">The authentication service used to create JWT bearer tokens</param>
        public LoginController(IReadOperation<Username, UserEntity> userStore,
            IPasswordHasher passwordHasher, IJwtAuthenticationService jwtAuthenticationService)
        {
            this._userStore = userStore;
            this._passwordHasher = passwordHasher;
            this._jwtAuthenticationService = jwtAuthenticationService;
        }

        /// <summary>
        /// Creates a JWT bearer token after successful authentication
        /// </summary>
        /// <param name="credentials">The login credentials to validate</param>
        /// <returns>A JWT token if the <paramref name="credentials"/> were valid, or an <see cref="UnauthorizedResult"/></returns>
        [HttpPost]
        public async Task<IActionResult> CreateJwtToken([FromBody] LoginCredentials credentials)
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