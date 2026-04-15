using System.Linq.Expressions;
using BaseProject.Models.Contracts.Dtos;
using BaseProject.Models.Data;

namespace BaseProject.Models.Mappers;

public static class AisleMapper
{
    
    public static Expression<Func<Aisle, AisleDto>> ToDtoExpression =>
        a => new AisleDto
        {
            Number = a.Number,
            Name = a.Name,
            TotalRack = a.TotalRack
        };

    public static Aisle ToEntity(this AisleDto item, string wareHouseCode) => new()
    {
        WareHouseCode = wareHouseCode,
        Number = item.Number,
        Name = item.Name,
        TotalRack = item.TotalRack,
    };
    

    public static AisleDto ToDto(this Aisle entity) => new()
    {
        Number = entity.Number,
        Name = entity.Name,
        TotalRack = entity.TotalRack
    };

    // Este método recibe el padre para establecer la relación
    public static Aisle ToEntity(this AisleDto dto, WareHouse warehouse)
    {
        return new Aisle
        {
            Number = dto.Number,
            Name = dto.Name,
            TotalRack = dto.TotalRack,
            WareHouseInfo = warehouse,
            WareHouseCode = warehouse.Code
        };
    }
}