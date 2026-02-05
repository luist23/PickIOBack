using BaseProject.Data;
using BaseProject.Models.Contracts;
using BaseProject.Models.Contracts.Dtos;
using BaseProject.Models.Contracts.Responses;
using BaseProject.Models.Data;
using BaseProject.Models.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BaseProject.Services.Data;

public class ProviderService(ProjectDbContext context)
{
    public IOrderedQueryable<Provider> GetAll(ProviderFilter filter)
    {
        var query = context.Providers.AsQueryable();
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
    
    public async Task<ResultResponse> GetByCode(string code)
    {
        var provider = await context.Providers.FindAsync(code);
        if (provider == null)
        {
            return new ResultResponse.Error("Provider not found");
        }
        return new ResultResponse.Success<Provider>(provider);
    }
    
    public async Task<ResultResponse> Create(ProviderDto dto)
    {
        if (await context.Providers.AnyAsync(x => x.Code == dto.Code))
        {
            return new ResultResponse.Error($"Provider {dto.Code} already exists");
        }

        var provider = dto.ToEntity();
        
        await context.Providers.AddAsync(provider);
        await context.SaveChangesAsync();
        
        return new ResultResponse.Success<Provider>(provider);
    }
    
    public async Task<ResultResponse> Update(string code, ProviderDto dto)
    {
        var existing = await context.Providers.FindAsync(code);
        if (existing == null)
        {
            return new ResultResponse.Error("Provider not found");
        }
        
        existing.Name = dto.Name;
        
        context.Providers.Update(existing);
        await context.SaveChangesAsync();
        
        return new ResultResponse.Success<Provider>(existing);
    }
    
    public async Task<ResultResponse> Delete(string code)
    {
        var existing = await context.Providers.FindAsync(code);
        if (existing == null)
        {
            return new ResultResponse.Error("Provider not found");
        }
        existing.Delete(existing.DeleteAt == null);
        await context.SaveChangesAsync();
        
        return new ResultResponse.Success<Provider>(existing);
    }
    
    public async Task<ResultResponse> Destroy(string code)
    {
        var existing = await context.Providers.FindAsync(code);
        if (existing == null)
        {
            return new ResultResponse.Error("Provider not found");
        }
        context.Providers.Remove(existing);
        await context.SaveChangesAsync();
        
        return new ResultResponse.Success<string>("Provider deleted");
    }
}
