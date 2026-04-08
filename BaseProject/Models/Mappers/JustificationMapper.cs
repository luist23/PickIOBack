using System.Linq.Expressions;
using BaseProject.Models.Contracts.Dtos;
using BaseProject.Models.Data;

namespace BaseProject.Models.Mappers;

public static class JustificationMapper
{
    public static Expression<Func<Justification, JustificationDto>> Projection
        => x => x.ToDto();

    public static JustificationDto ToDto(this Justification item) => new()
    {
        Id = item.Id,
        Name = item.Name,
        Active = item.DeleteAt == null
    };

    public static Justification ToEntity(this JustificationDto item) => new()
    {
        Id = item.Id,
        Name = item.Name,
    };
}
