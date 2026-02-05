using System.Linq.Expressions;
using BaseProject.Models.Contracts.Dtos;
using BaseProject.Models.Data;

namespace BaseProject.Models.Mappers;

public static class BarCodeMapper
{
    public static Expression<Func<BarCode, BarCodeDto>> Projection => x => new BarCodeDto
    {
        Code = x.Code,
        InternalCode = x.InternalCode
    };

    public static BarCodeDto ToDto(this BarCode barCode)
    {
        return new BarCodeDto
        {
            Code = barCode.Code,
            InternalCode = barCode.InternalCode
        };
    }
}
