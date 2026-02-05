using System.Collections.ObjectModel;
using BaseProject.Models.Data;
using BaseProject.Models.Enums;

namespace BaseProject.Models.Contracts.Dtos;

public class PurchaseOrderDto
{
    public int Id { get; set; }
    public string ProviderCode { get; set; } = string.Empty;
    public string? Invoice { get; set; }
    public long? Transference { get; set; }
    public string? TransferenceUser { get; set; }
    public long? Sync { get; set; }
    public string? SyncUser { get; set; }
    public OrderStatus Status { get; set; }
    public string? UserId { get; set; }

    public List<PurchaseProductDto> Products { get; set; } = [];

    public PurchaseOrderDto()
    {
    }

    public PurchaseOrderDto(PurchaseOrder purchaseOrder)
    {
        Id = purchaseOrder.Id;
        ProviderCode = purchaseOrder.ProviderCode;
        Invoice = purchaseOrder.Invoice;
        Transference = purchaseOrder.Transference;
        TransferenceUser = purchaseOrder.TransferenceUser;
        Sync = purchaseOrder.Sync;
        SyncUser = purchaseOrder.SyncUser;
        Status = purchaseOrder.Status;
        UserId = purchaseOrder.UserId;
        Products = purchaseOrder.Products.Select(p => new PurchaseProductDto(p)).ToList();
    }

    public PurchaseOrder ToEntity()
    {
        return new PurchaseOrder
        {
            Id = Id,
            ProviderCode = ProviderCode,
            Invoice = Invoice,
            Transference = Transference,
            TransferenceUser = TransferenceUser,
            Sync = Sync,
            SyncUser = SyncUser,
            Status = Status,
            UserId = UserId,
            Products = new Collection<PurchaseProduct>(Products.Select(p => p.ToEntity()).ToList())
        };
    }
}