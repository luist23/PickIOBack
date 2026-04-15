using System.Linq.Expressions;
using BaseProject.Models.Contracts.Dtos;
using BaseProject.Models.Data;

namespace BaseProject.Models.Mappers;

public static class BarCodeMapper
{
    public static Expression<Func<BarCode, BarCodeDto>> Projection
        => x => x.ToDto();

    public static BarCodeDto ToDto(this BarCode item) => new()
    {
        Code = item.Code,
        InternalCode = item.InternalCode,
        Active = item.DeletedAt == null
    };
    
    public static BarCode ToEntity(this BarCodeDto item) => new()
    {
        Code = item.Code,
        InternalCode = item.InternalCode,
    };
}