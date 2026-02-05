using BaseProject.Models.Data;
using BaseProject.Models.Enums;

namespace BaseProject.Models.Contracts.Dtos;

public class SaleOrderStatusDto
{
    public int IdOrder { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime Time { get; set; }
    public string UserId { get; set; } = string.Empty;

    public SaleOrderStatusDto()
    {
    }
    
    // Note: SaleOrderStatus is an entity, but here we treat it mainly as input/output data.
    public SaleOrderStatusDto(SaleOrderStatus status)
    {
        IdOrder = status.IdOrder;
        Status = status.Status;
        Time = status.Time;
        UserId = status.UserId;
    }

    public SaleOrderStatus ToEntity()
    {
        return new SaleOrderStatus
        {
            IdOrder = IdOrder,
            Status = Status,
            Time = Time,
            UserId = UserId
        };
    }
}
