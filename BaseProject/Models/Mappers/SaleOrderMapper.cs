using System.Linq.Expressions;
using BaseProject.Models.Contracts.Dtos;
using BaseProject.Models.Data;

namespace BaseProject.Models.Mappers;

public static class SaleOrderMapper
{
    public static Expression<Func<SaleOrder, SaleOrderDto>> Projection => x => new SaleOrderDto
    {
        Id = x.Id,
        CustomerCode = x.CustomerCode,
        Transference = x.Transference,
        TransferenceUser = x.TransferenceUser,
        Sync = x.Sync,
        SyncUser = x.SyncUser,
        UserId = x.UserId,
        Status = x.Status,
        Active = x.DeletedAt == null,
        Products = x.Products.Select(p => new SaleProductDto
        {
            ItemCode = p.ItemCode,
            AmountRequest = p.AmountRequest,
            AmountDispatch = p.AmountDispatch,
            Justify = p.Justify,
            Adjustment = p.Adjustment,
            TimeToExpire = p.TimeToExpire
        }).ToList(),
        ProductSerials = x.ProductSerials.Select(s => new SaleProductSerialDto
        {
            ItemCode = s.ItemCode,
            Serial = s.Serial
        }).ToList()
    };

    public static SaleOrderDto ToDto(this SaleOrder saleOrder)
    {
        return new SaleOrderDto(saleOrder);
    }
}
