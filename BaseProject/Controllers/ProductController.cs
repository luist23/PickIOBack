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
[Route(Routes.ProductApiRoute)]
public class ProductController(ProductService service) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProductDto>), 200)]
    public JsonResult Get([FromQuery] ProductFilter request)
    {
        var query = service.GetAll(request)
            .Select(ProductMapper.Projection);
        return ProjectController.RespondPagination(query, request);
    }

    [HttpGet("{code}")]
    [ProducesResponseType(typeof(ProductDto), 200)]
    [ProducesResponseType(typeof(IEnumerable<ApiError>), 400)]
    public async Task<JsonResult> Get(string code)
    {
        var result = await service.GetByCode(code);
        if (result is ResultResponse.Success<Product> success)
        {
            return ProjectController.Respond(success.Result.ToDto());
        }

        return ProjectController.JsonResponse<Product>(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ProductDto), 200)]
    [ProducesResponseType(typeof(IEnumerable<ApiError>), 400)]
    public async Task<JsonResult> Create([FromBody] ProductDto dto)
    {
        var result = await service.Create(dto);
        if (result is ResultResponse.Success<Product> success)
            return ProjectController.Respond(success.Result.ToDto());

        return ProjectController.JsonResponse<Product>(result);
    }

    [HttpPut("{code}")]
    [ProducesResponseType(typeof(ProductDto), 200)]
    [ProducesResponseType(typeof(IEnumerable<ApiError>), 400)]
    public async Task<JsonResult> Update(string code, [FromBody] ProductDto dto)
    {
        var result = await service.Update(code, dto);
        if (result is ResultResponse.Success<Product> success)
            return ProjectController.Respond(success.Result.ToDto());

        return ProjectController.JsonResponse<Product>(result);
    }

    [HttpDelete("{code}")]
    [ProducesResponseType(typeof(ProductDto), 200)]
    [ProducesResponseType(typeof(IEnumerable<ApiError>), 400)]
    public async Task<JsonResult> Delete(string code)
    {
        var result = await service.Delete(code);
        if (result is ResultResponse.Success<Product> success)
            return ProjectController.Respond(success.Result.ToDto());

        return ProjectController.JsonResponse<Product>(result);
    }

    [HttpDelete("destroy/{code}")]
    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(typeof(IEnumerable<ApiError>), 400)]
    public async Task<JsonResult> Destroy(string code)
    {
        return ProjectController.JsonResponse<string>(await service.Destroy(code));
    }
}
