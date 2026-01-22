using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using BaseProject.Data;
using Microsoft.EntityFrameworkCore;
using BaseProject.Configuration;

namespace BaseProject.Models.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ValidateSessionAttribute : Attribute, IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;
        if (user.Identity?.IsAuthenticated != true)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var sessionToken = user.FindFirst("session_token")?.Value;
        if (string.IsNullOrEmpty(sessionToken))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var dbContext = context.HttpContext.RequestServices.GetRequiredService<ProjectDbContext>();
        var settings = context.HttpContext.RequestServices.GetRequiredService<ProjectAppSettings>();

        var session = await dbContext.UserSessions
            .FirstOrDefaultAsync(s => s.Token == sessionToken)
            ;

        if (session == null || session.Expired < DateTime.UtcNow)
        {
            context.Result = new UnauthorizedObjectResult(new { message = "Sesión expirada o inválida" });
            return;
        }

        session.LastActivity = DateTime.UtcNow;
        session.Expired = DateTime.UtcNow.AddMinutes(settings.Jwt.SessionExpirationMinutes);

        await dbContext
            .SaveChangesAsync()
            ;
    }
}