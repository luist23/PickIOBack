using System.Collections.ObjectModel;
using BaseProject.Models.Data;

namespace BaseProject.Models.Contracts.Dtos;

public class WareHouseDto
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public List<Aisle> Aisles { set; get; } = [];

    public WareHouseDto()
    {
    }

    public WareHouseDto(WareHouse wareHouse)
    {
        Code = wareHouse.Code;
        Name = wareHouse.Name;
        Aisles = wareHouse.Aisles.ToList();
    }

    public WareHouse ToEntity()
    {
        return new WareHouse
        {
            Code = Code,
            Name = Name,
            Aisles = new Collection<Aisle>(Aisles)
        };
    }
}
