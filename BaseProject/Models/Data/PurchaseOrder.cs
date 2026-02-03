using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseProject.Models.Attributes;
using BaseProject.Models.Enums;
using BaseProject.Models.Utils;

namespace BaseProject.Models.Data;

[Table(nameof(PurchaseOrder))]
public class PurchaseOrder : TimeStampedModel
{
    #region Attibutes

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }

    [AttMaxLength(Provider.CodeLength)]
    [AttRequired]
    public string ProviderCode { get; set; } = string.Empty;

    [AttMaxLength(25)] public string? Invoice { get; set; } //Credito fiscal; numero de factura; a llenar (requerido)

    public long? Transference { get; set; }
    [AttMaxLength(User.UserIdLength)] public string? TransferenceUser { get; set; }

    public long? Sync { get; set; }
    [AttMaxLength(User.UserIdLength)] public string? SyncUser { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    [AttMaxLength(User.UserIdLength)] public string? UserId { set; get; }

    #endregion

    #region Relations

    public virtual Collection<PurchaseProduct> Products { get; set; } = [];

    #endregion


    public void UpdateSync(string syncUser)
    {
        SyncUser = syncUser;
        Sync = TimeUtil.GetTimeLong();
    }
}