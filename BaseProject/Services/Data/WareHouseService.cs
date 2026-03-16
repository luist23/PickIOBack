using BaseProject.Data;
using BaseProject.Models.Contracts;
using BaseProject.Models.Contracts.Dtos;
using BaseProject.Models.Contracts.Responses;
using BaseProject.Models.Data;
using BaseProject.Models.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BaseProject.Services.Data;

public class WareHouseService(ProjectDbContext context)
{
    public IOrderedQueryable<WareHouse> GetAll(WareHouseFilter filter)
    {
        var query = context.WareHouses
            .Include(e=> e.Aisles)
            .AsQueryable();
        var search = filter.Search;
        var lastSync = filter.LastSync;
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(x => x.Code.Contains(search) || x.Name.Contains(search));
        }
        
        if (lastSync.HasValue)
        {
             query = query.Where(x => x.UpdateAt > lastSync.Value);
        }

        return query.OrderBy(x => x.Code);
    }
    
    public ResultResponse GetByCode(string code)
    {
        var wareHouse = context.WareHouses
            .Include(e=> e.Aisles)
            .FirstOrDefault(e=> e.Code == code);
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
        var existing = await context.WareHouses.FindAsync(code);
        if (existing == null)
        {
            return new ResultResponse.Error("WareHouse not found");
        }
        
        existing.Name = dto.Name;
        
        context.WareHouses.Update(existing);
        await context.SaveChangesAsync();
        
        return new ResultResponse.Success<WareHouse>(existing);
    }
    
    public async Task<ResultResponse> Delete(string code)
    {
        var existing = await context.WareHouses.FindAsync(code);
        if (existing == null)
        {
            return new ResultResponse.Error("WareHouse not found");
        }
        existing.Delete(existing.DeleteAt == null);
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
