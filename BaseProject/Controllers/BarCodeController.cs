using BaseProject.Configuration;
using BaseProject.Models.Contracts;
using BaseProject.Models.Contracts.Dtos;
using BaseProject.Models.Data;
using BaseProject.Services.Data;
using Microsoft.AspNetCore.Mvc;

namespace BaseProject.Controllers;

[ApiController]
[Route(Routes.BarcodeApiRoute)]
public class BarCodeController(BarCodeService service) : ControllerBase
{
    [HttpGet]
    public JsonResult Get([FromQuery] BarCodeFilter request)
    {
        return ProjectController.RespondPagination(service.GetAll(request), request);
    }

    [HttpGet("{code}")]
    public async Task<JsonResult> Get(string code)
    {
        return ProjectController.JsonResponse<BarCode>(await service.GetByCode(code));
    }

    [HttpPost]
    public async Task<JsonResult> Create([FromBody] BarCodeDto barcode)
    {
        return ProjectController.JsonResponse<BarCode>(await service.Create(barcode));
    }

    [HttpPut("{code}")]
    public async Task<JsonResult> Update(string code, [FromBody] BarCodeDto barcode)
    {
        return ProjectController.JsonResponse<BarCode>(await service.Update(code, barcode));
    }

    [HttpDelete("{code}")]
    public async Task<JsonResult> Delete(string code)
    {
        return ProjectController.JsonResponse<string>(await service.Delete(code));
    }

    [HttpDelete("destroy/{code}")]
    public async Task<JsonResult> Destroy(string code)
    {
        return ProjectController.JsonResponse<string>(await service.Destroy(code));
    }
}