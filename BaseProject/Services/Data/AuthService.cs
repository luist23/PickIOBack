using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BaseProject.Configuration;
using BaseProject.Models.Contracts.Responses;
using BaseProject.Models.Data;
using BaseProject.Models.Requests;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using BaseProject.Data;
using Microsoft.EntityFrameworkCore;

namespace BaseProject.Services.Data;

public class AuthService(
    UserManager<User> userManager,
    SignInManager<User> signInManager,
    ProjectAppSettings settings,
    ProjectDbContext context
)
{
    public async Task<ResultResponse> Login(LoginRequest request)
    {
        var user = await userManager.FindByNameAsync(request.UserName);
        if (user == null)
            return new ResultResponse.ErrorApi("Usuario no encontrado", nameof(request.UserName));

        var result = await signInManager
            .CheckPasswordSignInAsync(user, request.Password, false);

        if (!result.Succeeded) return new ResultResponse.ErrorApi("Credenciales inválidas", nameof(request.Password));

        var sessionToken = Guid.NewGuid().ToString("N");

        // Gestionar sesiones activas
        var activeSessions = await context.UserSessions
            .Where(x => x.UserId == user.Id)
            .OrderBy(x => x.LastActivity)
            .ToListAsync();

        if (activeSessions.Count >= settings.Jwt.MaxActiveSessions)
        {
            // Eliminar la más antigua (por actividad)
            var oldestSession = activeSessions.First();
            context.UserSessions.Remove(oldestSession);
        }

        // Crear nueva sesión
        var newSession = new UserSession
        {
            UserId = user.Id,
            Token = sessionToken,
            Expired = DateTime.UtcNow.AddMinutes(settings.Jwt.SessionExpirationMinutes),
            LastActivity = DateTime.UtcNow,
            DeviceInfo = "Unknown" // Podría venir del request
        };

        context.UserSessions.Add(newSession);
        await context.SaveChangesAsync();

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Name, user.UserName ?? ""),
            new("session_token", sessionToken)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Jwt.Key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var jwt = new JwtSecurityToken(
            issuer: settings.Jwt.Issuer,
            audience: settings.Jwt.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(settings.Jwt.TokenExpirationMinutes),
            signingCredentials: credentials
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(jwt);
        return new ResultResponse.Success<string>(tokenString);
    }

    public async Task<ResultResponse> Logout(string userName, string sessionToken)
    {
        var user = await userManager.FindByIdAsync(userName);
        if (user == null) return new ResultResponse.ErrorApi("Usuario no encontrado", nameof(User.UserName));

        var session = context.UserSessions
            .FirstOrDefault(s => s.UserId == user.Id && s.Token == sessionToken);

        if (session == null) return new ResultResponse.Success<string>("Sesión cerrada");

        context.UserSessions.Remove(session);
        await context.SaveChangesAsync();

        return new ResultResponse.Success<string>("Sesión cerrada");
    }
}