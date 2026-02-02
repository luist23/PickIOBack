using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseProject.Models.Attributes;
using BaseProject.Models.Utils;

namespace BaseProject.Models.Data;

[Table("warehouse")]
public class WareHouse :  TimeStampedModel
{
    public const int CodeLength = 5;
    public const int NameLength = 50;

    #region Attibutes

    [Key]
    [AttMaxLength(CodeLength)]
    [AttRequired]
    public string Code { set; get; } = string.Empty;

    [AttRequired]
    [AttMaxLength(NameLength)]
    public string Name { set; get; } = string.Empty;

    #endregion

    #region Relations

    public virtual Collection<Aisle> Aisles { set; get; } = [];

    #endregion
    
}