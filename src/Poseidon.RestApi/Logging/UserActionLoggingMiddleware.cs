using Microsoft.AspNetCore.Http.Extensions;
using Poseidon.RestApi.Logins;
using Poseidon.RestApi.Users;

namespace Poseidon.RestApi.Logging
{
    public class UserActionLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IJwtAuthenticationService _jwtAuthenticationService;
        private ILogger<UserActionLoggingMiddleware> _logger;

        public UserActionLoggingMiddleware(RequestDelegate next,
            IJwtAuthenticationService jwtAuthenticationService,
            ILogger<UserActionLoggingMiddleware> logger)
        {
            _next = next;
            _jwtAuthenticationService = jwtAuthenticationService;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var principal = context.User;

            var user = this._jwtAuthenticationService.ParseUserData(principal);

            if (user is not null)
                _logger.LogInformation(WriteUserActionToLog(context, user));

            await _next(context);
        }

        private static string WriteUserActionToLog(HttpContext context, IUserData user) =>
            $"A User with the Id '{user.Id}' and the role '{user.Role}' performed a '{context.Request.Method}' to the endpoint '{context.Request.GetDisplayUrl()}'.";
    }
}