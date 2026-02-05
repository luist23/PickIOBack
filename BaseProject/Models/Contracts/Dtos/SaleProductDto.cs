using BaseProject.Models.Data;

namespace BaseProject.Models.Contracts.Dtos;

public class SaleProductDto
{
    public string ItemCode { get; set; } = string.Empty;
    public int AmountRequest { get; set; }
    public int AmountDispatch { get; set; }
    public int? Justify { get; set; }
    public int Adjustment { get; set; }
    public long? TimeToExpire { get; set; }

    public SaleProductDto()
    {
    }

    public SaleProductDto(SaleProduct product)
    {
        ItemCode = product.ItemCode;
        AmountRequest = product.AmountRequest;
        AmountDispatch = product.AmountDispatch;
        Justify = product.Justify;
        Adjustment = product.Adjustment;
        TimeToExpire = product.TimeToExpire;
    }

    public SaleProduct ToEntity()
    {
        return new SaleProduct
        {
            ItemCode = ItemCode,
            AmountRequest = AmountRequest,
            AmountDispatch = AmountDispatch,
            Justify = Justify,
            Adjustment = Adjustment,
            TimeToExpire = TimeToExpire
        };
    }
}
