using BaseProject.Configuration;
using BaseProject.Models.Contracts;
using BaseProject.Models.Contracts.Dtos;
using BaseProject.Models.Contracts.Responses;
using BaseProject.Models.Data;
using BaseProject.Models.Mappers;
using BaseProject.Services.Data;
using Microsoft.AspNetCore.Mvc;

namespace BaseProject.Controllers;

[ApiController]
[Route(Routes.ProviderApiRoute)]
public class ProviderController(ProviderService service) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProviderDto>), 200)]
    public JsonResult Get([FromQuery] ProviderFilter request)
    {
        var query = service.GetAll(request)
            .Select(ProviderMapper.Projection);
        return ProjectController.RespondPagination(query, request);
    }

    [HttpGet("{code}")]
    [ProducesResponseType(typeof(ProviderDto), 200)]
    [ProducesResponseType(typeof(IEnumerable<ApiError>), 400)]
    public async Task<JsonResult> Get(string code)
    {
        var result = await service.GetByCode(code);
        if (result is ResultResponse.Success<Provider> success)
        {
            return ProjectController.Respond(success.Result.ToDto());
        }

        return ProjectController.JsonResponse<Provider>(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ProviderDto), 200)]
    [ProducesResponseType(typeof(IEnumerable<ApiError>), 400)]
    public async Task<JsonResult> Create([FromBody] ProviderDto dto)
    {
        var result = await service.Create(dto);
        if (result is ResultResponse.Success<Provider> success)
            return ProjectController.Respond(success.Result.ToDto());

        return ProjectController.JsonResponse<Provider>(result);
    }

    [HttpPut("{code}")]
    [ProducesResponseType(typeof(ProviderDto), 200)]
    [ProducesResponseType(typeof(IEnumerable<ApiError>), 400)]
    public async Task<JsonResult> Update(string code, [FromBody] ProviderDto dto)
    {
        var result = await service.Update(code, dto);
        if (result is ResultResponse.Success<Provider> success)
            return ProjectController.Respond(success.Result.ToDto());

        return ProjectController.JsonResponse<Provider>(result);
    }

    [HttpDelete("{code}")]
    [ProducesResponseType(typeof(ProviderDto), 200)]
    [ProducesResponseType(typeof(IEnumerable<ApiError>), 400)]
    public async Task<JsonResult> Delete(string code)
    {
        var result = await service.Delete(code);
        if (result is ResultResponse.Success<Provider> success)
            return ProjectController.Respond(success.Result.ToDto());

        return ProjectController.JsonResponse<Provider>(result);
    }

    [HttpDelete("destroy/{code}")]
    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(typeof(IEnumerable<ApiError>), 400)]
    public async Task<JsonResult> Destroy(string code)
    {
        return ProjectController.JsonResponse<string>(await service.Destroy(code));
    }

    [HttpGet("count")]
    [ProducesResponseType(typeof(ProviderCountResponse), 200)]
    public async Task<JsonResult> Count([FromQuery] ProviderFilter request)
    {
        var total = await service.CountAsync(request);
        return ProjectController.Respond(new ProviderCountResponse { Total = total });
    }
}
