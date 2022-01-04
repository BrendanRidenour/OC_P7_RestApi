using Microsoft.AspNetCore.Http.Extensions;
using Poseidon.RestApi.Logins;
using Poseidon.RestApi.Users;

namespace Poseidon.RestApi.Logging
{
    /// <summary>
    /// A class used for logging authenticated <see cref="UserEntity"/> actions
    /// </summary>
    public class UserActionLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IJwtAuthenticationService _jwtAuthenticationService;
        private ILogger<UserActionLoggingMiddleware> _logger;

        /// <summary>
        /// Instantiates the class
        /// </summary>
        /// <param name="next">The next command in the middleware pipeline</param>
        /// <param name="jwtAuthenticationService">The authentication service required to parse <see cref="UserEntity"/> information</param>
        /// <param name="logger">The logger required to log authenticated <see cref="UserEntity"/> actions</param>
        public UserActionLoggingMiddleware(RequestDelegate next,
            IJwtAuthenticationService jwtAuthenticationService,
            ILogger<UserActionLoggingMiddleware> logger)
        {
            _next = next;
            _jwtAuthenticationService = jwtAuthenticationService;
            _logger = logger;
        }

        /// <summary>
        /// Performs <see cref="UserEntity"/> logging if one is authenticated
        /// </summary>
        /// <param name="context">The context of the current HTTP request</param>
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