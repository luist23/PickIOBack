using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseProject.Models.Attributes;
using BaseProject.Models.Enums;

namespace BaseProject.Models.Data;

[Table("product")]
public class Product : TimeStampedModel
{
    #region Attibutes

    [AttMaxLength(25)] [AttRequired] [Key] public string Code { set; get; } = string.Empty;
    [AttMaxLength(50)] [AttRequired] public string Name { set; get; } = string.Empty;
    [AttMaxLength(150)] [AttRequired] public string Detail { set; get; } = string.Empty;
    [AttMaxLength(20)] public string? Location { set; get; } // whareHouseCode AisleNumber RackNumber TypeRack(A,B) Level
    [AttRequired] public ProductType Type { set; get; } = ProductType.Default;

    #endregion

   
}