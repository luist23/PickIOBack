using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseProject.Models.Attributes;

namespace BaseProject.Models.Data;

[Table(nameof(PurchaseProduct))]
public class PurchaseProduct 
{
    #region Attibutes

    [Key, Column(Order = 0)] public int PurchaseOrderId { get; set; }

    [Key, Column(Order = 1)]
    [AttMaxLength(Product.CodeLength)]
    public string ItemCode { get; set; } = string.Empty;

    public int AmountRequest { get; set; }
    public int AmountDispatch { get; set; }
    [AttMaxLength(150)] public int? Justify { get; set; }
    public int Adjustment { get; set; }

    #endregion

    #region Relations

    public virtual Product? ProductInfo { set; get; }

    #endregion

   
}