using System.Collections.ObjectModel;
using BaseProject.Models.Attributes;
using BaseProject.Models.Data;

namespace BaseProject.Models.Contracts.Dtos;

public class WareHouseDto
{
    [AttMaxLength(WareHouse.CodeLength)]
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public bool Active { get; set; }
    public List<AisleDto> Aisles { set; get; } = [];
}
