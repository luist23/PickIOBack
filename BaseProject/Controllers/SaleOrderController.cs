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
    #region Get All

    [HttpGet]
    [ProducesResponseType(typeof(ApiPaginationResponse<SaleOrderDto>), 200)]
    public JsonResult Get([FromQuery] SaleOrderFilter request)
    {
        var query = service.GetAll(request)
            .Select(SaleOrderMapper.Projection);
        return ProjectController.RespondPagination(query, request);
    }

    [HttpGet("pending")]
    [ProducesResponseType(typeof(ApiPaginationResponse<SaleOrderDto>), 200)]
    public JsonResult GetPending([FromQuery] SaleOrderFilter request)
    {
        var query = service.GetAll(request, true)
            .Select(SaleOrderMapper.Projection);
        return ProjectController.RespondPagination(query, request);
    }

    #endregion

    #region Get Id

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<SaleOrderDto>), 200)]
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

    #endregion

    [HttpPost]
    [ProducesResponseType(typeof(SaleOrderDto), 200)]
    [ProducesResponseType(typeof(IEnumerable<ApiError>), 400)]
    public async Task<JsonResult> Create([FromBody] SaleOrderDto dto)
    {
        var result = await service.Create(dto);
        if (result is ResultResponse.Success<SaleOrder> success)
            return ProjectController.Respond(success.Result.ToDto());

        return ProjectController.JsonResponse<SaleOrder>(result);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(SaleOrderDto), 200)]
    [ProducesResponseType(typeof(IEnumerable<ApiError>), 400)]
    public async Task<JsonResult> Update(int id, [FromBody] SaleOrderDto dto)
    {
        var result = await service.Update(id, dto);
        if (result is ResultResponse.Success<SaleOrder> success)
            return ProjectController.Respond(success.Result.ToDto());

        return ProjectController.JsonResponse<SaleOrder>(result);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(SaleOrderDto), 200)]
    [ProducesResponseType(typeof(IEnumerable<ApiError>), 400)]
    public async Task<JsonResult> Delete(int id)
    {
        var result = await service.Delete(id);
        if (result is ResultResponse.Success<SaleOrder> success)
            return ProjectController.Respond(success.Result.ToDto());

        return ProjectController.JsonResponse<SaleOrder>(result);
    }

    [HttpDelete("destroy/{id:int}")]
    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(typeof(IEnumerable<ApiError>), 400)]
    public async Task<JsonResult> Destroy(int id)
    {
        return ProjectController.JsonResponse<string>(await service.Destroy(id));
    }

    #region Count

    [HttpGet("count")]
    [ProducesResponseType(typeof(ApiResponse<SaleOrderCountResponse>), 200)]
    public async Task<JsonResult> Count([FromQuery] SaleOrderFilter request)
    {
        var total = await service.CountAsync(request);
        return ProjectController.Respond(new SaleOrderCountResponse { Total = total });
    }

    #endregion
}