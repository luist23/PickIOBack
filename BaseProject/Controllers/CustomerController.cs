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
[Route(Routes.CustomerApiRoute)]
public class CustomerController(CustomerService service) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CustomerDto>), 200)]
    public JsonResult Get([FromQuery] CustomerFilter request)
    {
        var query = service.GetAll(request)
            .Select(CustomerMapper.Projection);
        return ProjectController.RespondPagination(query, request);
    }

    [HttpGet("{code}")]
    [ProducesResponseType(typeof(CustomerDto), 200)]
    [ProducesResponseType(typeof(IEnumerable<ApiError>), 400)]
    public async Task<JsonResult> Get(string code)
    {
        var result = await service.GetByCode(code);
        if (result is ResultResponse.Success<Customer> success)
        {
            return ProjectController.Respond(success.Result.ToDto());
        }

        return ProjectController.JsonResponse<Customer>(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CustomerDto), 200)]
    [ProducesResponseType(typeof(IEnumerable<ApiError>), 400)]
    public async Task<JsonResult> Create([FromBody] CustomerDto dto)
    {
        var result = await service.Create(dto);
        if (result is ResultResponse.Success<Customer> success)
            return ProjectController.Respond(success.Result.ToDto());

        return ProjectController.JsonResponse<Customer>(result);
    }

    [HttpPut("{code}")]
    [ProducesResponseType(typeof(CustomerDto), 200)]
    [ProducesResponseType(typeof(IEnumerable<ApiError>), 400)]
    public async Task<JsonResult> Update(string code, [FromBody] CustomerDto dto)
    {
        var result = await service.Update(code, dto);
        if (result is ResultResponse.Success<Customer> success)
            return ProjectController.Respond(success.Result.ToDto());

        return ProjectController.JsonResponse<Customer>(result);
    }

    [HttpDelete("{code}")]
    [ProducesResponseType(typeof(CustomerDto), 200)]
    [ProducesResponseType(typeof(IEnumerable<ApiError>), 400)]
    public async Task<JsonResult> Delete(string code)
    {
        var result = await service.Delete(code);
        if (result is ResultResponse.Success<Customer> success)
            return ProjectController.Respond(success.Result.ToDto());

        return ProjectController.JsonResponse<Customer>(result);
    }

    [HttpDelete("destroy/{code}")]
    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(typeof(IEnumerable<ApiError>), 400)]
    public async Task<JsonResult> Destroy(string code)
    {
        return ProjectController.JsonResponse<string>(await service.Destroy(code));
    }

    [HttpGet("count")]
    [ProducesResponseType(typeof(ApiResponse<CustomerCountResponse>), 200)]
    public async Task<JsonResult> Count([FromQuery] CustomerFilter request)
    {
        var total = await service.CountAsync(request);
        return ProjectController.Respond(new CustomerCountResponse { Total = total });
    }
}
