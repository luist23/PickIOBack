using System.Linq.Expressions;
using BaseProject.Models.Contracts.Dtos;
using BaseProject.Models.Data;

namespace BaseProject.Models.Mappers;

public static class AisleMapper
{
    public static Expression<Func<Aisle, AisleDto>> Projection
        => x => x.ToDto();

    public static AisleDto ToDto(this Aisle item) => new()
    {
        TotalRack = item.TotalRack,
        Name = item.Name,
        Number = item.Number,
    };

    public static Aisle ToEntity(this AisleDto item, string wareHouseCode) => new()
    {
        WareHouseCode = wareHouseCode,
        Number = item.Number,
        Name = item.Name,
        TotalRack = item.TotalRack,
    };
}