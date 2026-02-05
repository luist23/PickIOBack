using BaseProject.Models.Data;

namespace BaseProject.Models.Contracts.Dtos;

public class WareHouseDto
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    public WareHouseDto()
    {
    }

    public WareHouseDto(WareHouse wareHouse)
    {
        Code = wareHouse.Code;
        Name = wareHouse.Name;
    }

    public WareHouse ToEntity()
    {
        return new WareHouse
        {
            Code = Code,
            Name = Name
        };
    }
}
