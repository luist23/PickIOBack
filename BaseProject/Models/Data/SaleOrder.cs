using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseProject.Models.Attributes;
using BaseProject.Models.Enums;
using BaseProject.Models.Utils;

namespace BaseProject.Models.Data;

[Table(nameof(SaleOrder))]
public class SaleOrder : TimeStampedModel
{
    #region Attibutes

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }

    [AttMaxLength(Customer.CodeLength)]
    [AttRequired]
    public string CustomerCode { get; set; } = string.Empty;

    public long? Transference { get; set; }
    [AttMaxLength(User.UserIdLength)] public string? TransferenceUser { get; set; }
    public long? Sync { get; set; }
    [AttMaxLength(User.UserIdLength)] public string? SyncUser { get; set; }

    [AttMaxLength(User.UserIdLength)] public string? UserId { set; get; }

    public OrderStatus Status { get; set; }

    #endregion

    #region Relations

    public virtual Collection<SaleProduct> Products { get; set; } = [];

    public virtual Collection<SaleProductSerial> ProductSerials { get; set; } = [];
    public virtual Customer? CustomerInfo { get; set; }
    public virtual User? UserInfo { get; set; }

    #endregion

    #region SettingModel

    public void UpdateSync(string syncUser)
    {
        SyncUser = syncUser;
        Sync = TimeUtil.GetTimeLong();
    }

    #endregion
    
}