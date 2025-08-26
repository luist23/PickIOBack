using System.Security.Claims;
using BaseProject.Models.Requests;
using BaseProject.Models.Responses;
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
        return ResultResponse.JsonResponse(res);
    }

    [HttpPost("logout")]
    [Authorize]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(List<string>), StatusCodes.Status401Unauthorized)]
    public async Task<JsonResult> Logout()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return ResultResponse.ErrorResponse("No se encontro usuario", 401);
        var res = await authService.Logout(userId).ConfigureAwait(false);
        return ResultResponse.JsonResponse(res);
    }
}