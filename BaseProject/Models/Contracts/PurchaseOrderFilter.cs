using BaseProject.Models.Enums;

namespace BaseProject.Models.Contracts;

public class PurchaseOrderFilter : PaginationRequest
{
    public long? LastSync { get; set; }
    public OrderStatus? Status { get; set; }
}
