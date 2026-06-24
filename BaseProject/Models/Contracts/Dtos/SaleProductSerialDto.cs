using BaseProject.Models.Data;
using BaseProject.Models.Enums;

namespace BaseProject.Models.Contracts.Dtos;

public class SaleProductSerialDto
{
    public string ItemCode { get; set; } = string.Empty;
    public string Serial { get; set; } = string.Empty;

    public ProductSerialStatus Status { get; set; } = ProductSerialStatus.Pending;

    public string? Comments { get; set; } = string.Empty;

    public string? ReplacementSerial { get; set; }

    public SaleProductSerialDto()
    {
    }

    public SaleProductSerialDto(SaleProductSerial serial)
    {
        ItemCode = serial.ItemCode;
        Serial = serial.Serial;
        Status = serial.Status;
        Comments = serial.Comments;
    }

    public SaleProductSerial ToEntity()
    {
        return new SaleProductSerial
        {
            ItemCode = ItemCode,
            Serial = Serial,
            Status = Status,
            Comments = Comments,
        };
    }
}