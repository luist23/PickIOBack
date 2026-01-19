using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BaseProject.Configuration;
using BaseProject.Models.Contracts.Responses;
using BaseProject.Models.Data;
using BaseProject.Models.Requests;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace BaseProject.Services.Data;

public class AuthService(UserManager<User> userManager, SignInManager<User> signInManager, ProjectAppSettings settings)
{
    public async Task<ResultResponse> Login(LoginRequest request)
    {
        var user = await userManager.FindByNameAsync(request.UserName).ConfigureAwait(false);
        if (user == null)
            return new ResultResponse.ErrorApi("Usuario no encontrado", nameof(request.UserName));

        var result = await signInManager
            .CheckPasswordSignInAsync(user, request.Password, false)
            .ConfigureAwait(false);

        if (!result.Succeeded) return new ResultResponse.ErrorApi("Credenciales inválidas", nameof(request.Password));

        var sessionToken = Guid.NewGuid().ToString("N");
        user.SessionToken = sessionToken;
        user.SessionTokenExpiry = DateTime.UtcNow.AddMinutes(settings.Jwt.SessionExpirationMinutes);

        await userManager.UpdateAsync(user).ConfigureAwait(false);

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

    public async Task<ResultResponse> Logout(string userName)
    {
        var user = await userManager.FindByIdAsync(userName).ConfigureAwait(false);
        if (user == null) return new ResultResponse.ErrorApi("Usuario no encontrado", nameof(User.UserName));

        user.SessionToken = null;
        user.SessionTokenExpiry = null;
        await userManager.UpdateAsync(user).ConfigureAwait(false);

        return new ResultResponse.Success<string>("Sesión cerrada");
    }
}