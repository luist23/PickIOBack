using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseProject.Models.Attributes;
using BaseProject.Models.Enums;

namespace BaseProject.Models.Data;

[Table(nameof(Product))]
public class Product : TimeStampedModel
{
    public const int CodeLength = 25;

    #region Attibutes

    [AttMaxLength(CodeLength)] [AttRequired] [Key] public string Code { set; get; } = string.Empty;
    [AttMaxLength(50)] [AttRequired] public string Name { set; get; } = string.Empty;
    [AttMaxLength(150)] [AttRequired] public string Detail { set; get; } = string.Empty;
    [AttMaxLength(20)] public string? Location { set; get; } // whareHouseCode AisleNumber RackNumber TypeRack(A,B) Level 00004-21-23-A/B-99

    #endregion

   
}