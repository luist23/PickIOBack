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
[Route(Routes.BarcodeApiRoute)]
public class BarCodeController(BarCodeService service) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<BarCodeDto>), 200)]
    public JsonResult Get([FromQuery] BarCodeFilter request)
    {
        var query = service.GetAll(request)
            .Select(BarCodeMapper.Projection);
        return ProjectController.RespondPagination(query, request);
    }

    [HttpGet("{code}")]
    [ProducesResponseType(typeof(BarCodeDto), 200)]
    [ProducesResponseType(typeof(IEnumerable<ApiError>), 400)]
    public async Task<JsonResult> Get(string code)
    {
        var result = await service.GetByCode(code);
        if (result is ResultResponse.Success<BarCode> success)
            return ProjectController.Respond(success.Result.ToDto());

        return ProjectController.JsonResponse<BarCode>(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(BarCode), 200)]
    [ProducesResponseType(typeof(IEnumerable<ApiError>), 400)]
    public async Task<JsonResult> Create([FromBody] BarCodeDto barcode)
    {
        return ProjectController.JsonResponse<BarCode>(await service.Create(barcode));
    }

    [HttpPut("{code}")]
    [ProducesResponseType(typeof(BarCode), 200)]
    [ProducesResponseType(typeof(IEnumerable<ApiError>), 400)]
    public async Task<JsonResult> Update(string code, [FromBody] BarCodeDto barcode)
    {
        return ProjectController.JsonResponse<BarCode>(await service.Update(code, barcode));
    }

    [HttpDelete("{code}")]
    [ProducesResponseType(typeof(BarCode), 200)]
    [ProducesResponseType(typeof(IEnumerable<ApiError>), 400)]
    public async Task<JsonResult> Delete(string code)
    {
        return ProjectController.JsonResponse<BarCode>(await service.Delete(code));
    }

    [HttpDelete("destroy/{code}")]
    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(typeof(IEnumerable<ApiError>), 400)]
    public async Task<JsonResult> Destroy(string code)
    {
        return ProjectController.JsonResponse<string>(await service.Destroy(code));
    }
}