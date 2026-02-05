using System.Linq.Expressions;
using BaseProject.Models.Contracts.Dtos;
using BaseProject.Models.Data;

namespace BaseProject.Models.Mappers;

public static class WareHouseMapper
{
    public static Expression<Func<WareHouse, WareHouseDto>> Projection => x => new WareHouseDto
    {
        Code = x.Code,
        Name = x.Name
    };

    public static WareHouseDto ToDto(this WareHouse wareHouse)
    {
        return new WareHouseDto
        {
            Code = wareHouse.Code,
            Name = wareHouse.Name
        };
    }
}
