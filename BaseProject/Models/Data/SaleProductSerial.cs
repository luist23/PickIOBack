using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseProject.Models.Attributes;

namespace BaseProject.Models.Data;

[Table("sale_product_serial")]
public class SaleProductSerial
{
    [Key, Column(Order = 0)] public int SaleOrderId { get; set; }

    [AttMaxLength(25)]
    [Key, Column(Order = 1)]
    public string ItemCode { get; set; } = string.Empty;

    [Key, Column(Order = 2)]
    [Required]
    [AttMaxLength(50)]
    public string Serial { get; set; } = string.Empty;
}