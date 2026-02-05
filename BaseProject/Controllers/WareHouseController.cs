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
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<WareHouseDto>), 200)]
    public JsonResult Get([FromQuery] WareHouseFilter request)
    {
        var query = service.GetAll(request)
            .Select(WareHouseMapper.Projection);
        return ProjectController.RespondPagination(query, request);
    }

    [HttpGet("{code}")]
    [ProducesResponseType(typeof(WareHouseDto), 200)]
    [ProducesResponseType(typeof(IEnumerable<ApiError>), 400)]
    public async Task<JsonResult> Get(string code)
    {
        var result = await service.GetByCode(code);
        if (result is ResultResponse.Success<WareHouse> success)
        {
            return ProjectController.Respond(success.Result.ToDto());
        }

        return ProjectController.JsonResponse<WareHouse>(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(WareHouse), 200)]
    [ProducesResponseType(typeof(IEnumerable<ApiError>), 400)]
    public async Task<JsonResult> Create([FromBody] WareHouseDto dto)
    {
        return ProjectController.JsonResponse<WareHouse>(await service.Create(dto));
    }

    [HttpPut("{code}")]
    [ProducesResponseType(typeof(WareHouse), 200)]
    [ProducesResponseType(typeof(IEnumerable<ApiError>), 400)]
    public async Task<JsonResult> Update(string code, [FromBody] WareHouseDto dto)
    {
        return ProjectController.JsonResponse<WareHouse>(await service.Update(code, dto));
    }

    [HttpDelete("{code}")]
    [ProducesResponseType(typeof(WareHouse), 200)]
    [ProducesResponseType(typeof(IEnumerable<ApiError>), 400)]
    public async Task<JsonResult> Delete(string code)
    {
        return ProjectController.JsonResponse<WareHouse>(await service.Delete(code));
    }

    [HttpDelete("destroy/{code}")]
    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(typeof(IEnumerable<ApiError>), 400)]
    public async Task<JsonResult> Destroy(string code)
    {
        return ProjectController.JsonResponse<string>(await service.Destroy(code));
    }
}
