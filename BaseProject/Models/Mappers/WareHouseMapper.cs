using System.Linq.Expressions;
using BaseProject.Models.Contracts.Dtos;
using BaseProject.Models.Data;

namespace BaseProject.Models.Mappers;

public static class WareHouseMapper
{
    public static Expression<Func<WareHouse, WareHouseDto>> Projection
        => x => x.ToDto();

    public static WareHouseDto ToDto(this WareHouse item) => new()
    {
        Code = item.Code,
        Name = item.Name,
        Active = item.DeleteAt == null
    };

    public static WareHouse ToEntity(this WareHouseDto item) => new()
    {
        Code = item.Code,
        Name = item.Name,
    };
}
