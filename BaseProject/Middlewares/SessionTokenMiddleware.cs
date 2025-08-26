using System.Security.Claims;
using BaseProject.Models.Data;
using Microsoft.AspNetCore.Identity;

namespace BaseProject.Middlewares;

public class SessionTokenMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context, UserManager<User> userManager)
    {
        if (context.User.Identity?.IsAuthenticated == true)
        {
            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var sessionToken = context.User.FindFirstValue("session_token");

            if (userId == null || sessionToken == null)
            {
                await context.Response.WriteAsync("Sesión inválida").ConfigureAwait(false);
                return;
            }

            var user = await userManager.FindByIdAsync(userId).ConfigureAwait(false);
            if (user == null || user.SessionToken != sessionToken || user.SessionTokenExpiry < DateTime.UtcNow)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Sesión expirada o inválida").ConfigureAwait(false);
                return;
            }
        }

        await next(context).ConfigureAwait(false);
    }
}