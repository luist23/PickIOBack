using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseProject.Models.Attributes;

namespace BaseProject.Models.Data;

[Table("barcode")]
public class BarCode : TimeStampedModel
{
    #region Attibutes

    [Key]
    [AttRequired]
    [AttMinLength(4)]
    [AttMaxLength(150)]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string Code { get; set; } = string.Empty;

    [AttRequired]
    [AttMinLength(4)]
    [AttMaxLength(150)]
    public string InternalCode { get; set; } = string.Empty;

    #endregion
    
}