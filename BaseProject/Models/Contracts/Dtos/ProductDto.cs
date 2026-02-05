using BaseProject.Models.Data;
using BaseProject.Models.Enums;

namespace BaseProject.Models.Contracts.Dtos;

public class ProductDto
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Detail { get; set; } = string.Empty;
    public string? Location { get; set; }
    public ProductType Type { get; set; }

    public ProductDto()
    {
    }

    public ProductDto(Product product)
    {
        Code = product.Code;
        Name = product.Name;
        Detail = product.Detail;
        Location = product.Location;
        Type = product.Type;
    }

    public Product ToEntity()
    {
        return new Product
        {
            Code = Code,
            Name = Name,
            Detail = Detail,
            Location = Location,
            Type = Type
        };
    }
}
