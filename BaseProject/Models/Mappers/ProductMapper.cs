using System.Linq.Expressions;
using BaseProject.Models.Contracts.Dtos;
using BaseProject.Models.Data;

namespace BaseProject.Models.Mappers;

public static class ProductMapper
{
    public static Expression<Func<Product, ProductDto>> Projection
        => x => x.ToDto();

    public static ProductDto ToDto(this Product item) => new()
    {
        Code = item.Code,
        Name = item.Name,
        Detail = item.Detail,
        Location = item.Location,
        Active = item.DeleteAt == null
    };

    public static Product ToEntity(this ProductDto item) => new()
    {
        Code = item.Code,
        Name = item.Name,
        Detail = item.Detail,
        Location = item.Location
    };
}
