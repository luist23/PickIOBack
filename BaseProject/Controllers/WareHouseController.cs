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
[Route(Routes.WareHouseApiRoute)]
public class WareHouseController(WareHouseService service) : ControllerBase
{
    #region Get All

    [HttpGet]
    [ProducesResponseType(typeof(ApiPaginationResponse<WareHouseDto>), 200)]
    public JsonResult Get([FromQuery] WareHouseFilter request)
    {
        var query = service.GetAll(request)
            .Select(WareHouseMapper.Projection);
        return ProjectController.RespondPagination(query, request);
    }

    #endregion

    [HttpGet("{code}")]
    [ProducesResponseType(typeof(WareHouseDto), 200)]
    [ProducesResponseType(typeof(IEnumerable<ApiError>), 400)]
    public Task<JsonResult> Get(string code)
    {
        var result = service.GetByCode(code);
        if (result is ResultResponse.Success<WareHouse> success)
        {
            return Task.FromResult(ProjectController.Respond(success.Result.ToDto()));
        }

        return Task.FromResult(ProjectController.JsonResponse<WareHouse>(result));
    }

    [HttpPost]
    [ProducesResponseType(typeof(WareHouseDto), 200)]
    [ProducesResponseType(typeof(IEnumerable<ApiError>), 400)]
    public async Task<JsonResult> Create([FromBody] WareHouseDto dto)
    {
        var result = await service.Create(dto);
        if (result is ResultResponse.Success<WareHouse> success)
            return ProjectController.Respond(success.Result.ToDto());

        return ProjectController.JsonResponse<WareHouse>(result);
    }

    [HttpPut("{code}")]
    [ProducesResponseType(typeof(WareHouseDto), 200)]
    [ProducesResponseType(typeof(IEnumerable<ApiError>), 400)]
    public async Task<JsonResult> Update(string code, [FromBody] WareHouseDto dto)
    {
        var result = await service.Update(code, dto);
        if (result is ResultResponse.Success<WareHouse> success)
            return ProjectController.Respond(success.Result.ToDto());

        return ProjectController.JsonResponse<WareHouse>(result);
    }

    [HttpDelete("{code}")]
    [ProducesResponseType(typeof(WareHouseDto), 200)]
    [ProducesResponseType(typeof(IEnumerable<ApiError>), 400)]
    public async Task<JsonResult> Delete(string code)
    {
        var result = await service.Delete(code);
        if (result is ResultResponse.Success<WareHouse> success)
            return ProjectController.Respond(success.Result.ToDto());

        return ProjectController.JsonResponse<WareHouse>(result);
    }

    [HttpDelete("destroy/{code}")]
    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(typeof(IEnumerable<ApiError>), 400)]
    public async Task<JsonResult> Destroy(string code)
    {
        return ProjectController.JsonResponse<string>(await service.Destroy(code));
    }

    #region Count

    [HttpGet("count")]
    [ProducesResponseType(typeof(ApiResponse<WareHouseCountResponse>), 200)]
    public async Task<JsonResult> Count([FromQuery] WareHouseFilter request)
    {
        var total = await service.CountAsync(request);
        return ProjectController.Respond(new WareHouseCountResponse { Total = total });
    }

    #endregion
}