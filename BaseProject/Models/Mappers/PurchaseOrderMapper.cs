using System.Linq.Expressions;
using BaseProject.Models.Contracts.Dtos;
using BaseProject.Models.Data;

namespace BaseProject.Models.Mappers;

public static class PurchaseOrderMapper
{
    public static Expression<Func<PurchaseOrder, PurchaseOrderDto>> Projection => x => new PurchaseOrderDto
    {
        Id = x.Id,
        ProviderCode = x.ProviderCode,
        Invoice = x.Invoice,
        Transference = x.Transference,
        TransferenceUser = x.TransferenceUser,
        Sync = x.Sync,
        SyncUser = x.SyncUser,
        Status = x.Status,
        UserId = x.UserId,
        Active = x.DeletedAt == null,
        Products = x.Products.Select(p => new PurchaseProductDto
        {
            ItemCode = p.ItemCode,
            AmountRequest = p.AmountRequest,
            AmountDispatch = p.AmountDispatch,
            Justify = p.Justify,
            Adjustment = p.Adjustment
        }).ToList()
    };

    public static PurchaseOrderDto ToDto(this PurchaseOrder purchaseOrder)
    {
        return new PurchaseOrderDto(purchaseOrder);
    }
}
