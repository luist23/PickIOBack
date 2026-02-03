using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseProject.Models.Attributes;
using BaseProject.Models.Utils;

namespace BaseProject.Models.Data;

[Table(nameof(WareHouse))]
public class WareHouse :  TimeStampedModel
{
    #region Constants

    public const int CodeLength = 5;
    public const int NameLength = 50;

    #endregion

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