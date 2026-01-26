using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseProject.Models.Interfaces;
using BaseProject.Models.Attributes;
using BaseProject.Models.Enums;
using BaseProject.Models.Utils;

namespace BaseProject.Models.Data;

[Table("purchaseorder")]
public class PurchaseOrder :  IHasTimestamps
{
    #region Attibutes

    [Key] 
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }
    [AttMaxLength(16)] [AttRequired] public string ProviderCode { get; set; } = string.Empty;

    [AttMaxLength(25)] public string? Invoice { get; set; } //Credito fiscal; numero de factura; a llenar (requerido)

    public long? Transference { get; set; }
    [AttMaxLength(50)] public string? TransferenceUser { get; set; }

    public long? Sync { get; set; }
    [AttMaxLength(50)] public string? SyncUser { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    [AttMaxLength(50)] public string? User { set; get; }
    
    public long UpdateAt { get; set; }
    public long CreateAt { get; set; }

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