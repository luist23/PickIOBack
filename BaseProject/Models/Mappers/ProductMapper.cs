using System.Linq.Expressions;
using BaseProject.Models.Contracts.Dtos;
using BaseProject.Models.Data;

namespace BaseProject.Models.Mappers;

public static class ProductMapper
{
    public static Expression<Func<Product, ProductDto>> Projection => x => new ProductDto
    {
        Code = x.Code,
        Name = x.Name,
        Detail = x.Detail,
        Location = x.Location,
        Type = x.Type
    };

    public static ProductDto ToDto(this Product product)
    {
        return new ProductDto
        {
            Code = product.Code,
            Name = product.Name,
            Detail = product.Detail,
            Location = product.Location,
            Type = product.Type
        };
    }
}
