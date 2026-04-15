using BaseProject.Data;
using BaseProject.Models.Contracts;
using BaseProject.Models.Contracts.Dtos;
using BaseProject.Models.Contracts.Responses;
using BaseProject.Models.Data;
using BaseProject.Models.Mappers;
using Microsoft.EntityFrameworkCore;

namespace BaseProject.Services.Data;

public class WareHouseService(ProjectDbContext context)
{
    public IOrderedQueryable<WareHouse> GetAll(WareHouseFilter filter)
    {
        IQueryable<WareHouse> query = context.WareHouses
            .Include(e => e.Aisles);

        var search = filter.Search;
        var lastSync = filter.LastSync;
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(x => x.Code.Contains(search) || x.Name.Contains(search));
        }

        if (lastSync.HasValue)
        {
            query = query.Where(x => x.UpdatedAt > lastSync.Value);
        }

        return query.OrderBy(x => x.Code);
    }

    public ResultResponse GetByCode(string code)
    {
        var wareHouse = context.WareHouses
            .Include(e => e.Aisles)
            .FirstOrDefault(e => e.Code == code);
        if (wareHouse == null)
        {
            return new ResultResponse.Error("WareHouse not found");
        }

        return new ResultResponse.Success<WareHouse>(wareHouse);
    }

    public async Task<ResultResponse> Create(WareHouseDto dto)
    {
        if (await context.WareHouses.AnyAsync(x => x.Code == dto.Code))
        {
            return new ResultResponse.Error($"WareHouse {dto.Code} already exists");
        }

        var wareHouse = dto.ToEntity();

        await context.WareHouses.AddAsync(wareHouse);
        await context.SaveChangesAsync();

        return new ResultResponse.Success<WareHouse>(wareHouse);
    }

    public async Task<ResultResponse> Update(string code, WareHouseDto dto)
    {
        var warehouse = await context.WareHouses
            .Include(w => w.Aisles)
            .FirstOrDefaultAsync(w => w.Code == code);

        if (warehouse == null)
            return new ResultResponse.Error("WareHouse not found");

        warehouse.Name = dto.Name;
        SyncAisles(warehouse, dto.Aisles);

        await context.SaveChangesAsync();

        return new ResultResponse.Success<WareHouse>(warehouse);
    }

    private static void SyncAisles(WareHouse warehouse, List<AisleDto>? dtoAisles)
    {
        // Si no vienen aisles, eliminamos todos
        if (dtoAisles == null || dtoAisles.Count == 0)
        {
            warehouse.Aisles.Clear();
            return;
        }

        // Diccionarios para búsqueda rápida
        var existingByNumber = warehouse.Aisles.ToDictionary(a => a.Number);
        var incomingNumbers = dtoAisles.Select(a => a.Number).ToHashSet();

        // Actualizar/crear
        foreach (var dto in dtoAisles)
            if (existingByNumber.TryGetValue(dto.Number, out var existingAisle))
            {
                existingAisle.Name = dto.Name;
                existingAisle.TotalRack = dto.TotalRack;
            }
            else
                warehouse.Aisles.Add(dto.ToEntity(warehouse.Code));

        // 2. Eliminar los que ya no vienen en el DTO
        var aislesToRemove = warehouse.Aisles
            .Where(a => !incomingNumbers.Contains(a.Number))
            .ToList();

        foreach (var aisle in aislesToRemove) warehouse.Aisles.Remove(aisle);
    }

    public async Task<ResultResponse> Delete(string code)
    {
        var existing = await context.WareHouses.FindAsync(code);
        if (existing == null)
        {
            return new ResultResponse.Error("WareHouse not found");
        }

        existing.Delete(existing.DeletedAt == null);
        await context.SaveChangesAsync();

        return new ResultResponse.Success<WareHouse>(existing);
    }

    public async Task<ResultResponse> Destroy(string code)
    {
        var existing = await context.WareHouses.FindAsync(code);
        if (existing == null)
        {
            return new ResultResponse.Error("WareHouse not found");
        }

        context.WareHouses.Remove(existing);
        await context.SaveChangesAsync();

        return new ResultResponse.Success<string>("WareHouse deleted");
    }
}