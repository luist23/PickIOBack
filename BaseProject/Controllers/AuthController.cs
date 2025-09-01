using System.Security.Claims;
using BaseProject.Models.Contracts;
using BaseProject.Models.Requests;
using BaseProject.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaseProject.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(AuthService authService) : ControllerBase
{
    [HttpPost("login")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
    public async Task<JsonResult> Login([FromBody] LoginRequest request)
    {
        var res = await authService.Login(request: request).ConfigureAwait(false);
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
        if (userId == null) return ProjectController.Reject("No se encontro usuario", ApiCodes.ErrorCode.UnAuthorized);
        var res = await authService.Logout(userId).ConfigureAwait(false);
        return ProjectController.JsonResponse<string>(res);
    }
}