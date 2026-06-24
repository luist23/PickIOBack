using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseProject.Models.Attributes;
using BaseProject.Models.Enums;

namespace BaseProject.Models.Data;

[Table(nameof(SaleProductSerial))]
public class SaleProductSerial
{
    [Key, Column(Order = 0)] public int SaleOrderId { get; set; }

    [Key, Column(Order = 1)]
    [AttMaxLength(Product.CodeLength)]
    public string ItemCode { get; set; } = string.Empty;

    [Required]
    [AttMaxLength(50)]
    [Key, Column(Order = 2)]
    public string Serial { get; set; } = string.Empty;
    
    public ProductSerialStatus Status { get; set; } = ProductSerialStatus.Pending;
    
    [AttMaxLength(100)]
    public string? Comments { get; set; } = string.Empty;
    
    [AttMaxLength(50)]
    public string? ReplacementSerial { get; set; }
}