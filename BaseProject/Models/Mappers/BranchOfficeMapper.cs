using System.Linq.Expressions;
using BaseProject.Models.Contracts.Dtos;
using BaseProject.Models.Data;

namespace BaseProject.Models.Mappers;

public static class BranchOfficeMapper
{
    public static Expression<Func<BranchOffice, BranchOfficeDto>> Projection
        => x => x.ToDto();

    public static BranchOfficeDto ToDto(this BranchOffice item) => new()
    {
        Code = item.Code,
        Name = item.Name,
        Country = item.Country,
        Active = item.DeleteAt == null
    };

    public static BranchOffice ToEntity(this BranchOfficeDto item) => new()
    {
        Code = item.Code,
        Name = item.Name,
        Country = item.Country
    };
}