using System.Linq.Expressions;
using BaseProject.Models.Contracts.Dtos;
using BaseProject.Models.Data;

namespace BaseProject.Models.Mappers;

public static class JustificationMapper
{
    public static Expression<Func<Justification, JustificationDto>> Projection => x => new JustificationDto
    {
        Id = x.Id,
        Name = x.Name
    };

    public static JustificationDto ToDto(this Justification justification)
    {
        return new JustificationDto
        {
            Id = justification.Id,
            Name = justification.Name
        };
    }
}
