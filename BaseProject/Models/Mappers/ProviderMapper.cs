using System.Linq.Expressions;
using BaseProject.Models.Contracts.Dtos;
using BaseProject.Models.Data;

namespace BaseProject.Models.Mappers;

public static class ProviderMapper
{
    public static Expression<Func<Provider, ProviderDto>> Projection
        => x => x.ToDto();

    public static ProviderDto ToDto(this Provider item) => new()
    {
        Code = item.Code,
        Name = item.Name,
        Active = item.DeletedAt == null
    };

    public static Provider ToEntity(this ProviderDto item) => new()
    {
        Code = item.Code,
        Name = item.Name,
    };
}