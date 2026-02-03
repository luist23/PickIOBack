using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseProject.Models.Attributes;
using BaseProject.Models.Enums;

namespace BaseProject.Models.Data;

[Table(nameof(SaleOrderStatus))]
public class SaleOrderStatus
{
    #region Attibutes

    [Key] public int IdOrder { set; get; }
    public OrderStatus Status { set; get; }
    public DateTime Time { set; get; }
    [AttMaxLength(User.UserIdLength)] public required string UserId { set; get; }

    #endregion

    #region Relations

    public virtual User UserInfo { set; get; } = null!;

    #endregion
}