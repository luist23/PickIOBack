using System.Security.Claims;
using BaseProject.Data;
using BaseProject.Models.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BaseProject.Configuration;

namespace BaseProject.Middlewares;

public class SessionTokenMiddleware(RequestDelegate next, ProjectAppSettings settings)
{
    public async Task InvokeAsync(HttpContext context,  ProjectDbContext db)
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

            var session =  await db.UserSessions
                .FirstOrDefaultAsync(e=> e.UserId == userId && e.Token == sessionToken)
                .ConfigureAwait(false);

            if (session == null || session.Expired < DateTime.UtcNow)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Sesión expirada o inválida").ConfigureAwait(false);
                return;
            }

            session.LastActivity = DateTime.UtcNow;
            session.Expired = DateTime.UtcNow.AddMinutes(settings.Jwt.SessionExpirationMinutes);
            await db.SaveChangesAsync().ConfigureAwait(false);
        }

        await next(context).ConfigureAwait(false);
    }
}