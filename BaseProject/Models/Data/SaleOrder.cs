using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseProject.Models.Attributes;
using BaseProject.Models.Enums;
using BaseProject.Models.Utils;

namespace BaseProject.Models.Data;

[Table("saleorder")]
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
    [AttMaxLength(50)] public string? TransferenceUser { get; set; }
    public long? Sync { get; set; }
    [AttMaxLength(50)] public string? SyncUser { get; set; }

    [AttMaxLength(50)] public string? User { set; get; }

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