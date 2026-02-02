using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseProject.Models.Attributes;

namespace BaseProject.Models.Data;

[Table(nameof(SaleProduct))]
public class SaleProduct
{
    #region Attibutes

    [Key, Column(Order = 0)] public int SaleOrderId { get; set; }

    [AttMaxLength(25)]
    [Key, Column(Order = 1)]
    public string ItemCode { get; set; } = string.Empty;

    public int AmountRequest { get; set; }
    public int AmountDispatch { get; set; }
    public int? Justify { get; set; }
    public int Adjustment { get; set; }
    
    public long? TimeToExpire { set; get; }

    #endregion

    #region Relations

    public virtual Product? ProductInfo { set; get; }

    #endregion

   
}