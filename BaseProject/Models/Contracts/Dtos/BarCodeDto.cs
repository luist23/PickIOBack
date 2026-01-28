using BaseProject.Models.Data;

namespace BaseProject.Models.Contracts.Dtos;

public class BarCodeDto
{
    public string Code { get; set; } = string.Empty;
    public string InternalCode { get; set; } = string.Empty;

    public BarCodeDto()
    {
    }

    public BarCodeDto(BarCode barCode)
    {
        Code = barCode.Code;
        InternalCode = barCode.InternalCode;
    }

    public BarCode ToEntity()
    {
        return new BarCode
        {
            Code = Code,
            InternalCode = InternalCode
        };
    }
}
