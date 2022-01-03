using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;
using Poseidon.RestApi.Mocks;
using Poseidon.RestApi.Users;
using System.Threading.Tasks;
using Xunit;

namespace Poseidon.RestApi.Logging
{
    public class UserActionLoggingMiddlewareTests
    {
        [Fact]
        public async Task InvokeAsync_ParseUserDataReturnsNull_DoesNotWriteToLog()
        {
            var authService = JwtAuthenticationService();
            authService.ParseUserData_Result = null;
            var logger = Logger();
            var middleware = Middleware(jwtAuthenticationService: authService, logger: logger);
            var context = Context();

            await middleware.InvokeAsync(context);

            Assert.Null(logger.Log_InputLogLevel);
        }

        [Fact]
        public async Task InvokeAsync_ParseUserDataReturnsNull_CallsNext()
        {
            HttpContext? nextContext= null;
            var nextCalled = false;
            var authService = JwtAuthenticationService();
            authService.ParseUserData_Result = null;
            var middleware = Middleware(context =>
            {
                nextContext = context;
                nextCalled = true;

                return Task.CompletedTask;
            },  authService);
            var context = Context();

            await middleware.InvokeAsync(context);

            Assert.Equal(context, nextContext);
            Assert.True(nextCalled);
        }

        [Theory]
        [InlineData(1, "role1", "GET", "/path1")]
        [InlineData(2, "role2", "POST", "/path2")]
        public async Task InvokeAsync_ParseUserDataReturnsUser_WriteToLog(int id, string role,
            string httpMethod, string urlPath)
        {
            var authService = JwtAuthenticationService();
            authService.ParseUserData_Result = User(id, role);
            var logger = Logger();
            var middleware = Middleware(jwtAuthenticationService: authService, logger: logger);
            var context = Context(httpMethod, urlPath);

            await middleware.InvokeAsync(context);

            Assert.Equal(LogLevel.Information, logger.Log_InputLogLevel);
            Assert.Equal($"A User with the Id '{id}' and the role '{role}' performed a '{httpMethod}' to the endpoint '{context.Request.GetDisplayUrl()}'.",
                logger.Log_WrittenString);
        }

        [Theory]
        [InlineData(1, "role1", "GET", "/path1")]
        [InlineData(2, "role2", "POST", "/path2")]
        public async Task InvokeAsync_ParseUserDataReturnsUser_CallsNext(int id, string role,
            string httpMethod, string urlPath)
        {
            HttpContext? nextContext = null;
            var nextCalled = false;
            var authService = JwtAuthenticationService();
            authService.ParseUserData_Result = User(id, role);
            var middleware = Middleware(context =>
            {
                nextContext = context;
                nextCalled = true;

                return Task.CompletedTask;
            }, authService);
            var context = Context(httpMethod, urlPath);

            await middleware.InvokeAsync(context);

            Assert.Equal(context, nextContext);
            Assert.True(nextCalled);
        }

        private static IUserData User(int id, string role) => new UserData()
        {
            Id = id,
            Username = "username",
            Fullname = "Full Name",
            Role = role,
        };
        private static HttpContext Context(string httpMethod = "GET", string urlPath = "/")
        {
            var context = new DefaultHttpContext();

            context.Request.Method = httpMethod;
            context.Request.Scheme = "https";
            context.Request.Host = new HostString("example.com");
            context.Request.Path = new PathString(urlPath);

            return context;
        }
        private static RequestDelegate Next() => context => Task.CompletedTask;
        private static MockJwtAuthenticationService JwtAuthenticationService() =>
            new MockJwtAuthenticationService();
        private static MockLogger<UserActionLoggingMiddleware> Logger() =>
            new MockLogger<UserActionLoggingMiddleware>();
        private static UserActionLoggingMiddleware Middleware(RequestDelegate? next = null,
            MockJwtAuthenticationService? jwtAuthenticationService = null,
            MockLogger<UserActionLoggingMiddleware>? logger = null) =>
            new UserActionLoggingMiddleware(next ?? Next(),
                jwtAuthenticationService ?? JwtAuthenticationService(),
                logger ?? Logger());
    }
}