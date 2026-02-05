using BaseProject.Models.Data;

namespace BaseProject.Models.Contracts.Dtos;

public class PurchaseProductDto
{
    public string ItemCode { get; set; } = string.Empty;
    public int AmountRequest { get; set; }
    public int AmountDispatch { get; set; }
    public int? Justify { get; set; }
    public int Adjustment { get; set; }

    public PurchaseProductDto()
    {
    }

    public PurchaseProductDto(PurchaseProduct product)
    {
        ItemCode = product.ItemCode;
        AmountRequest = product.AmountRequest;
        AmountDispatch = product.AmountDispatch;
        Justify = product.Justify;
        Adjustment = product.Adjustment;
    }

    public PurchaseProduct ToEntity()
    {
        return new PurchaseProduct
        {
            ItemCode = ItemCode,
            AmountRequest = AmountRequest,
            AmountDispatch = AmountDispatch,
            Justify = Justify,
            Adjustment = Adjustment
        };
    }
}
