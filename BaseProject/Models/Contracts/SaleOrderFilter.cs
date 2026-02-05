using BaseProject.Models.Enums;

namespace BaseProject.Models.Contracts;

public class SaleOrderFilter : PaginationRequest
{
    public long? LastSync { get; set; }
    public OrderStatus? Status { get; set; }
}
