using System.Collections.ObjectModel;
using BaseProject.Models.Data;
using BaseProject.Models.Enums;

namespace BaseProject.Models.Contracts.Dtos;

public class SaleOrderDto
{
    public int Id { get; set; }
    public string CustomerCode { get; set; } = string.Empty;
    public long? Transference { get; set; }
    public string? TransferenceUser { get; set; }
    public long? Sync { get; set; }
    public string? SyncUser { get; set; }
    public string? UserId { get; set; }
    public OrderStatus Status { get; set; }

    public List<SaleProductDto> Products { get; set; } = [];
    public List<SaleProductSerialDto> ProductSerials { get; set; } = [];
    public List<SaleOrderStatusDto> StatusUpdates { get; set; } = [];

    public SaleOrderDto()
    {
    }

    public SaleOrderDto(SaleOrder saleOrder)
    {
        Id = saleOrder.Id;
        CustomerCode = saleOrder.CustomerCode;
        Transference = saleOrder.Transference;
        TransferenceUser = saleOrder.TransferenceUser;
        Sync = saleOrder.Sync;
        SyncUser = saleOrder.SyncUser;
        UserId = saleOrder.UserId;
        Status = saleOrder.Status;
        Products = saleOrder.Products.Select(p => new SaleProductDto(p)).ToList();
        ProductSerials = saleOrder.ProductSerials.Select(s => new SaleProductSerialDto(s)).ToList();
        // StatusUpdates are typically logs, not stored in SaleOrder object directly as a collection usually?
        // Checking SaleOrder model: it does not have a collection of SaleOrderStatus.
        // SaleOrderStatus is a separate table, potentially multiple entries per order or one current?
        // The Model SaleOrderStatus has "IdOrder" as Key? 
        // [Key] public int IdOrder { set; get; }
        // If IdOrder is PK, then it's 1:1 or 1:0..1? 
        // Let's re-read SaleOrderStatus.cs...
        // [Key] public int IdOrder { set; get; } -> It seems it only holds ONE status per order? Or is `[Key]` incorrect if it's a log?
        // User said: "hara registros segun cambie de estado, por lo que mandara esos items mas como registro. (en ningun momento se devolveran)"
        // If they are logs, they should probably have their own ID or composite key.
        // But for DTO purposes, we just list them here.
    }

    public SaleOrder ToEntity()
    {
        return new SaleOrder
        {
            Id = Id,
            CustomerCode = CustomerCode,
            Transference = Transference,
            TransferenceUser = TransferenceUser,
            Sync = Sync,
            SyncUser = SyncUser,
            UserId = UserId,
            Status = Status,
            Products = new Collection<SaleProduct>(Products.Select(p => p.ToEntity()).ToList()),
            ProductSerials = new Collection<SaleProductSerial>(ProductSerials.Select(s => s.ToEntity()).ToList())
            // StatusUpdates are not part of SaleOrder entity structure directly based on model definition provided.
            // They will be handled separately in Service.
        };
    }
}
