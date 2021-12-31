using Microsoft.AspNetCore.Mvc;
using Poseidon.RestApi.Mocks;
using Poseidon.RestApi.Users;
using System.Threading.Tasks;
using Xunit;
using static Poseidon.RestApi.TestHelpers;

namespace Poseidon.RestApi.Logins
{
    public class LoginControllerTests
    {
        [Fact]
        public void InheritsControllerBase()
        {
            Assert.True(typeof(ControllerBase)
                .IsAssignableFrom(typeof(LoginController)));
        }

        [Fact]
        public void HasApiControllerAttribute()
        {
            var attribute = GetClassAttribute<LoginController, ApiControllerAttribute>();

            Assert.NotNull(attribute);
        }

        [Fact]
        public void HasRouteAttribute()
        {
            var attribute = GetClassAttribute<LoginController, RouteAttribute>();

            Assert.NotNull(attribute);
            Assert.Equal("[controller]", attribute.Template);
        }

        [Fact]
        public async Task CreateJwtToken_UserStoreReturnsNull_ReturnsUnauthorized()
        {
            var userStore = UserStore();
            userStore.Read_Result = null;
            var controller = Controller(userStore);
            var credentials = Credentials();

            var result = await controller.CreateJwtToken(credentials);

            Assert.IsType<UnauthorizedResult>(result);
        }

        [Fact]
        public async Task CreateJwtToken_PasswordHasherValidateReturnsFalse_ReturnsUnauthorized()
        {
            var hasher = PasswordHasher();
            hasher.Validate_Result = false;
            var controller = Controller(passwordHasher: hasher);
            var credentials = Credentials();

            var result = await controller.CreateJwtToken(credentials);

            Assert.IsType<UnauthorizedResult>(result);
        }

        [Fact]
        public async Task CreateJwtToken_WhenCalled_CallsCreateTokenOnJwtAuthenticationService()
        {
            var userStore = UserStore();
            var auth = JwtAuthenticationService();
            var controller = Controller(userStore, jwtAuthenticationService: auth);
            var credentials = Credentials();

            await controller.CreateJwtToken(credentials);

            Assert.Equal(userStore.Read_Result, auth.CreateJwtToken_InputUser);
        }

        [Fact]
        public async Task CreateJwtToken_WhenCalled_ReturnsTokenFromJwtAuthenticationService()
        {
            var auth = JwtAuthenticationService();
            var controller = Controller(jwtAuthenticationService: auth);
            var credentials = Credentials();

            var actionResult = await controller.CreateJwtToken(credentials);

            var okResult = Assert.IsType<OkObjectResult>(actionResult);
            var value = okResult.Value!;
            Assert.Equal(auth.CreateJwtToken_Result,
                value.GetType().GetProperty("token")?.GetValue(value, null));
        }

        private static MockUserEntityReadOperation UserStore() =>
            new MockUserEntityReadOperation();
        private static MockPasswordHasher PasswordHasher() =>
            new MockPasswordHasher();
        private static MockJwtAuthenticationService JwtAuthenticationService() =>
            new MockJwtAuthenticationService();
        private static LoginController Controller(
            MockUserEntityReadOperation? userStore = null,
            MockPasswordHasher? passwordHasher = null,
            MockJwtAuthenticationService? jwtAuthenticationService = null) =>
            new LoginController(userStore ?? UserStore(),
                passwordHasher ?? PasswordHasher(),
                jwtAuthenticationService ?? JwtAuthenticationService());
        private static LoginCredentials Credentials(string username = "Username",
            string password = "P@ssw0rd") => new LoginCredentials()
            {
                Username = username,
                Password = password,
            };
        private static UserEntity UserEntity(string password) =>
            new UserEntity()
            {
                Username = "Username",
                Password = password,
                Fullname = "Full Name",
                Role = "Role",
            };
    }
}