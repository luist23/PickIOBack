using System.Linq.Expressions;
using BaseProject.Models.Contracts.Dtos;
using BaseProject.Models.Data;

namespace BaseProject.Models.Mappers;

public static class ProviderMapper
{
    public static Expression<Func<Provider, ProviderDto>> Projection => x => new ProviderDto
    {
        Code = x.Code,
        Name = x.Name
    };

    public static ProviderDto ToDto(this Provider provider)
    {
        return new ProviderDto
        {
            Code = provider.Code,
            Name = provider.Name
        };
    }
}
