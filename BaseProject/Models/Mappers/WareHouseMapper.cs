using System.Collections.ObjectModel;
using System.Linq.Expressions;
using BaseProject.Models.Contracts.Dtos;
using BaseProject.Models.Data;

namespace BaseProject.Models.Mappers;

public static class WareHouseMapper
{
    public static Expression<Func<WareHouse, WareHouseDto>> Projection =>
        x => new WareHouseDto
        {
            Code = x.Code,
            Name = x.Name,
            Active = x.DeletedAt == null, // Recomiendo cambiar a DeletedAt
            Aisles = x.Aisles
                .AsQueryable() // ← Esta es la clave
                .Select(AisleMapper.ToDtoExpression)
                .ToList()
        };

    // Mapeo Entidad → DTO
    public static WareHouseDto ToDto(this WareHouse entity) => new()
    {
        Code = entity.Code,
        Name = entity.Name,
        Active = entity.DeletedAt == null,
        Aisles = entity.Aisles?
            .Select(a => a.ToDto())
            .ToList() ?? new List<AisleDto>()
    };

    // Mapeo DTO → Entidad (para crear)
    public static WareHouse ToEntity(this WareHouseDto dto)
    {
        var warehouse = new WareHouse
        {
            Code = dto.Code,
            Name = dto.Name,
            // DeletedAt = null;  // normalmente se deja por defecto
        };

        if (dto.Aisles.Count != 0)
        {
            warehouse.Aisles = dto.Aisles
                .Select(a => a.ToEntity(warehouse)) // Pasamos la referencia del padre
                .ToList(); // List<Aisle> es más seguro
        }
        else
        {
            warehouse.Aisles = new List<Aisle>();
        }

        return warehouse;
    }
}