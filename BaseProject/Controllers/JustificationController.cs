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
[Route(Routes.JustificationApiRoute)]
public class JustificationController(JustificationService service) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<JustificationDto>), 200)]
    public JsonResult Get([FromQuery] JustificationFilter request)
    {
        var query = service.GetAll(request)
            .Select(JustificationMapper.Projection);
        return ProjectController.RespondPagination(query, request);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(JustificationDto), 200)]
    [ProducesResponseType(typeof(IEnumerable<ApiError>), 400)]
    public async Task<JsonResult> Get(int id)
    {
        var result = await service.GetById(id);
        if (result is ResultResponse.Success<Justification> success)
        {
            return ProjectController.Respond(success.Result.ToDto());
        }

        return ProjectController.JsonResponse<Justification>(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(JustificationDto), 200)]
    [ProducesResponseType(typeof(IEnumerable<ApiError>), 400)]
    public async Task<JsonResult> Create([FromBody] JustificationDto dto)
    {
        var result = await service.Create(dto);
        if (result is ResultResponse.Success<Justification> success)
            return ProjectController.Respond(success.Result.ToDto());

        return ProjectController.JsonResponse<Justification>(result);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(JustificationDto), 200)]
    [ProducesResponseType(typeof(IEnumerable<ApiError>), 400)]
    public async Task<JsonResult> Update(int id, [FromBody] JustificationDto dto)
    {
        var result = await service.Update(id, dto);
        if (result is ResultResponse.Success<Justification> success)
            return ProjectController.Respond(success.Result.ToDto());

        return ProjectController.JsonResponse<Justification>(result);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(JustificationDto), 200)]
    [ProducesResponseType(typeof(IEnumerable<ApiError>), 400)]
    public async Task<JsonResult> Delete(int id)
    {
        var result = await service.Delete(id);
        if (result is ResultResponse.Success<Justification> success)
            return ProjectController.Respond(success.Result.ToDto());

        return ProjectController.JsonResponse<Justification>(result);
    }

    [HttpDelete("destroy/{id:int}")]
    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(typeof(IEnumerable<ApiError>), 400)]
    public async Task<JsonResult> Destroy(int id)
    {
        return ProjectController.JsonResponse<string>(await service.Destroy(id));
    }
}
