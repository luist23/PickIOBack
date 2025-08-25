using BaseProject.Models.Requests;
using BaseProject.Models.Responses;
using BaseProject.Services.Data;
using Microsoft.AspNetCore.Mvc;

namespace BaseProject.Controllers;

public class AuthController(AuthService authService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<JsonResult> Login([FromBody] LoginRequest request)
    {
        var res = await authService.Login(request: request).ConfigureAwait(false);
        return ResultResponse.JsonResponse(res);
    }
}