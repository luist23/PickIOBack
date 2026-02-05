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
[Route(Routes.SaleOrderApiRoute)]
public class SaleOrderController(SaleOrderService service) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<SaleOrderDto>), 200)]
    public JsonResult Get([FromQuery] SaleOrderFilter request)
    {
        var query = service.GetAll(request)
            .Select(SaleOrderMapper.Projection);
        return ProjectController.RespondPagination(query, request);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(SaleOrderDto), 200)]
    [ProducesResponseType(typeof(IEnumerable<ApiError>), 400)]
    public async Task<JsonResult> Get(int id)
    {
        var result = await service.GetById(id);
        if (result is ResultResponse.Success<SaleOrder> success)
        {
            return ProjectController.Respond(success.Result.ToDto());
        }

        return ProjectController.JsonResponse<SaleOrder>(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(SaleOrder), 200)]
    [ProducesResponseType(typeof(IEnumerable<ApiError>), 400)]
    public async Task<JsonResult> Create([FromBody] SaleOrderDto dto)
    {
        return ProjectController.JsonResponse<SaleOrder>(await service.Create(dto));
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(SaleOrder), 200)]
    [ProducesResponseType(typeof(IEnumerable<ApiError>), 400)]
    public async Task<JsonResult> Update(int id, [FromBody] SaleOrderDto dto)
    {
        return ProjectController.JsonResponse<SaleOrder>(await service.Update(id, dto));
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(SaleOrder), 200)]
    [ProducesResponseType(typeof(IEnumerable<ApiError>), 400)]
    public async Task<JsonResult> Delete(int id)
    {
        return ProjectController.JsonResponse<SaleOrder>(await service.Delete(id));
    }

    [HttpDelete("destroy/{id:int}")]
    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(typeof(IEnumerable<ApiError>), 400)]
    public async Task<JsonResult> Destroy(int id)
    {
        return ProjectController.JsonResponse<string>(await service.Destroy(id));
    }
}
