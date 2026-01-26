using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseProject.Models.Attributes;
using BaseProject.Models.Interfaces;

namespace BaseProject.Models.Data;

[Table("barcode")]
public class BarCode : IHasTimestamps
{
    #region Attibutes

    [Key]
    [AttRequired]
    [AttMinLength(4)]
    [AttMaxLength(150)]
    public string Code { get; set; } = string.Empty;

    [AttRequired]
    [AttMinLength(4)]
    [AttMaxLength(150)]
    public string InternalCode { get; set; } = string.Empty;

   
    public long UpdateAt { get; set; }
    public long CreateAt { get; set; }
    public long? DeleteAt { get; set; }

    #endregion
    
}