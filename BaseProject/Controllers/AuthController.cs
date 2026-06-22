using System.Security.Claims;
using BaseProject.Configuration;
using BaseProject.Models.Contracts;
using BaseProject.Models.Requests;
using BaseProject.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaseProject.Controllers;

[ApiController]
[Route(Routes.AuthApiRoute)]
public class AuthController(AuthService authService) : ControllerBase
{
    [HttpPost("login")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
    public async Task<JsonResult> Login([FromBody] LoginRequest request)
    {
        var res = await authService.Login(request: request);
        return ProjectController.JsonResponse<string>(res);
    }

    [HttpPost("logout")]
    [Authorize]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(List<string>), StatusCodes.Status401Unauthorized)]
    public async Task<JsonResult> Logout()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var session = User.FindFirstValue("session_token");
        if (userId == null || session == null)
            return ProjectController.Reject("No se encontro usuario", ApiCodes.ErrorCode.UnAuthorized);
        var res = await authService.Logout(userId, session);
        return ProjectController.JsonResponse<string>(res);
    }
}