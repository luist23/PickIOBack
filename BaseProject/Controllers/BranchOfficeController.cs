using BaseProject.Configuration;
using BaseProject.Models.Contracts;
using BaseProject.Models.Contracts.Dtos;
using BaseProject.Models.Contracts.Responses;
using BaseProject.Models.Data;
using BaseProject.Services.Data;
using Microsoft.AspNetCore.Mvc;
using BaseProject.Models.Mappers;

namespace BaseProject.Controllers;

[ApiController]
[Route(Routes.BranchOfficeApiRoute)]
public class BranchOfficeController(BranchOfficeService service) : ControllerBase
{
    #region Get All

    [HttpGet]
    [ProducesResponseType(typeof(ApiPaginationResponse<BranchOfficeDto>), 200)]
    public JsonResult Get([FromQuery] BranchOfficeFilter request)
    {
        var query = service.GetAll(request)
            .Select(BranchOfficeMapper.Projection);
        return ProjectController.RespondPagination(query, request);
    }

    #endregion

    [HttpGet("{code}")]
    [ProducesResponseType(typeof(BranchOfficeDto), 200)]
    [ProducesResponseType(typeof(IEnumerable<ApiError>), 400)]
    public async Task<JsonResult> Get(string code)
    {
        var result = await service.GetByCode(code);
        if (result is ResultResponse.Success<BranchOffice> success)
            return ProjectController.Respond(success.Result.ToDto());

        return ProjectController.JsonResponse<BranchOffice>(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(BranchOfficeDto), 200)]
    [ProducesResponseType(typeof(IEnumerable<ApiError>), 400)]
    public async Task<JsonResult> Create([FromBody] BranchOfficeDto dto)
    {
        var result = await service.Create(dto);
        if (result is ResultResponse.Success<BranchOffice> success)
            return ProjectController.Respond(success.Result.ToDto());

        return ProjectController.JsonResponse<BranchOffice>(result);
    }

    [HttpPut("{code}")]
    [ProducesResponseType(typeof(BranchOfficeDto), 200)]
    [ProducesResponseType(typeof(IEnumerable<ApiError>), 400)]
    public async Task<JsonResult> Update(string code, [FromBody] BranchOfficeDto dto)
    {
        var result = await service.Update(code, dto);
        if (result is ResultResponse.Success<BranchOffice> success)
            return ProjectController.Respond(success.Result.ToDto());

        return ProjectController.JsonResponse<BranchOffice>(result);
    }

    [HttpDelete("{code}")]
    [ProducesResponseType(typeof(BranchOfficeDto), 200)]
    [ProducesResponseType(typeof(IEnumerable<ApiError>), 400)]
    public async Task<JsonResult> Delete(string code)
    {
        var result = await service.Delete(code);
        if (result is ResultResponse.Success<BranchOffice> success)
            return ProjectController.Respond(success.Result.ToDto());

        return ProjectController.JsonResponse<BranchOffice>(result);
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
    [ProducesResponseType(typeof(ApiResponse<BranchOfficeCountResponse>), 200)]
    public async Task<JsonResult> Count([FromQuery] BranchOfficeFilter request)
    {
        var total = await service.CountAsync(request);
        return ProjectController.Respond(new BranchOfficeCountResponse { Total = total });
    }

    #endregion
}