using System.ComponentModel.DataAnnotations.Schema;
using BaseProject.Models.Attributes;
using BaseProject.Models.Enums;

namespace BaseProject.Models.Data;

[Table("sale_order_status")]
public class SaleOrderStatus
{
    #region Attibutes

    public int IdOrder { set; get; }
    public OrderStatus Status { set; get; }
    public DateTime Time { set; get; }
    [AttMaxLength(50)] public required string User { set; get; }

    #endregion

    #region Relations

    public virtual User UserInfo { set; get; }

    #endregion

   
}