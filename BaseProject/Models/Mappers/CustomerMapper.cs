using System.Linq.Expressions;
using BaseProject.Models.Contracts.Dtos;
using BaseProject.Models.Data;

namespace BaseProject.Models.Mappers;

public static class CustomerMapper
{
    public static Expression<Func<Customer, CustomerDto>> Projection
        => x => x.ToDto();

    public static CustomerDto ToDto(this Customer item) => new()
    {
        Code = item.Code,
        Name = item.Name,
        Active = item.DeleteAt == null
    };

    public static Customer ToEntity(this CustomerDto item) => new()
    {
        Code = item.Code,
        Name = item.Name,
    };
}
