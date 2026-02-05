using System.Linq.Expressions;
using BaseProject.Models.Contracts.Dtos;
using BaseProject.Models.Data;

namespace BaseProject.Models.Mappers;

public static class BranchOfficeMapper
{
    public static Expression<Func<BranchOffice, BranchOfficeDto>> Projection => x => new BranchOfficeDto
    {
        Code = x.Code,
        Name = x.Name,
        Country = x.Country
    };

    public static BranchOfficeDto ToDto(this BranchOffice branchOffice)
    {
        return new BranchOfficeDto
        {
            Code = branchOffice.Code,
            Name = branchOffice.Name,
            Country = branchOffice.Country
        };
    }
}
