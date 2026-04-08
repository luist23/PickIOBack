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
[Route(Routes.PurchaseOrderApiRoute)]
public class PurchaseOrderController(PurchaseOrderService service) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PurchaseOrderDto>), 200)]
    public JsonResult Get([FromQuery] PurchaseOrderFilter request)
    {
        var query = service.GetAll(request)
            .Select(PurchaseOrderMapper.Projection);
        return ProjectController.RespondPagination(query, request);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(PurchaseOrderDto), 200)]
    [ProducesResponseType(typeof(IEnumerable<ApiError>), 400)]
    public async Task<JsonResult> Get(int id)
    {
        var result = await service.GetById(id);
        if (result is ResultResponse.Success<PurchaseOrder> success)
        {
            return ProjectController.Respond(success.Result.ToDto());
        }

        return ProjectController.JsonResponse<PurchaseOrder>(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(PurchaseOrderDto), 200)]
    [ProducesResponseType(typeof(IEnumerable<ApiError>), 400)]
    public async Task<JsonResult> Create([FromBody] PurchaseOrderDto dto)
    {
        var result = await service.Create(dto);
        if (result is ResultResponse.Success<PurchaseOrder> success)
            return ProjectController.Respond(success.Result.ToDto());

        return ProjectController.JsonResponse<PurchaseOrder>(result);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(PurchaseOrderDto), 200)]
    [ProducesResponseType(typeof(IEnumerable<ApiError>), 400)]
    public async Task<JsonResult> Update(int id, [FromBody] PurchaseOrderDto dto)
    {
        var result = await service.Update(id, dto);
        if (result is ResultResponse.Success<PurchaseOrder> success)
            return ProjectController.Respond(success.Result.ToDto());

        return ProjectController.JsonResponse<PurchaseOrder>(result);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(PurchaseOrderDto), 200)]
    [ProducesResponseType(typeof(IEnumerable<ApiError>), 400)]
    public async Task<JsonResult> Delete(int id)
    {
        var result = await service.Delete(id);
        if (result is ResultResponse.Success<PurchaseOrder> success)
            return ProjectController.Respond(success.Result.ToDto());

        return ProjectController.JsonResponse<PurchaseOrder>(result);
    }

    [HttpDelete("destroy/{id:int}")]
    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(typeof(IEnumerable<ApiError>), 400)]
    public async Task<JsonResult> Destroy(int id)
    {
        return ProjectController.JsonResponse<string>(await service.Destroy(id));
    }
}
