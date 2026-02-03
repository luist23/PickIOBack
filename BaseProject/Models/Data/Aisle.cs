using System.ComponentModel.DataAnnotations.Schema;
using BaseProject.Models.Attributes;

namespace BaseProject.Models.Data;

[Table(nameof(Aisle))]
public class Aisle
{
    private const int NameLength = 50;

    #region Attibutes

    [AttRequired]
    [AttMaxLength(WareHouse.CodeLength)]
    public string WareHouseCode { set; get; } = string.Empty;

    [AttRequired] [AttRange(1, 100)] public int Number { set; get; }

    [AttRequired]
    [AttMaxLength(NameLength)]
    public string Name { set; get; } = string.Empty;

    [AttRange(1, 100)] public int TotalRack { set; get; }

    #endregion

    #region Relations

    public virtual WareHouse? WareHouseInfo { set; get; }

    #endregion
}