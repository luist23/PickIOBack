using BaseProject.Models.Data;

namespace BaseProject.Models.Contracts.Dtos;

public class SaleProductSerialDto
{
    public string ItemCode { get; set; } = string.Empty;
    public string Serial { get; set; } = string.Empty;

    public SaleProductSerialDto()
    {
    }

    public SaleProductSerialDto(SaleProductSerial serial)
    {
        ItemCode = serial.ItemCode;
        Serial = serial.Serial;
    }

    public SaleProductSerial ToEntity()
    {
        return new SaleProductSerial
        {
            ItemCode = ItemCode,
            Serial = Serial
        };
    }
}
