using BaseProject.Data;
using BaseProject.Models.Contracts;
using BaseProject.Models.Contracts.Dtos;
using BaseProject.Models.Contracts.Responses;
using BaseProject.Models.Data;
using BaseProject.Models.Extensions;
using BaseProject.Models.Mappers;
using Microsoft.EntityFrameworkCore;

namespace BaseProject.Services.Data;

public class BranchOfficeService(ProjectDbContext context)
{
    public IOrderedQueryable<BranchOffice> GetAll(BranchOfficeFilter filter)
    {
        var query = context.BranchOffices.AsQueryable();
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
    
    public async Task<ResultResponse> GetByCode(string code)
    {
        var branchOffice = await context.BranchOffices.FindAsync(code);
        if (branchOffice == null)
        {
            return new ResultResponse.Error("BranchOffice not found");
        }
        return new ResultResponse.Success<BranchOffice>(branchOffice);
    }
    
    public async Task<ResultResponse> Create(BranchOfficeDto dto)
    {
        if (await context.BranchOffices.AnyAsync(x => x.Code == dto.Code))
        {
            return new ResultResponse.Error($"BranchOffice {dto.Code} already exists");
        }

        var branchOffice = dto.ToEntity();
        
        await context.BranchOffices.AddAsync(branchOffice);
        await context.SaveChangesAsync();
        
        return new ResultResponse.Success<BranchOffice>(branchOffice);
    }
    
    public async Task<ResultResponse> Update(string code, BranchOfficeDto dto)
    {
        var existing = await context.BranchOffices.FindAsync(code);
        if (existing == null)
        {
            return new ResultResponse.Error("BranchOffice not found");
        }
        
        existing.Name = dto.Name;
        existing.Country = dto.Country;
        
        context.BranchOffices.Update(existing);
        await context.SaveChangesAsync();
        
        return new ResultResponse.Success<BranchOffice>(existing);
    }
    
    public async Task<ResultResponse> Delete(string code)
    {
        var existing = await context.BranchOffices.FindAsync(code);
        if (existing == null)
        {
            return new ResultResponse.Error("BranchOffice not found");
        }
        existing.Delete(existing.DeletedAt == null);
        await context.SaveChangesAsync();
        
        return new ResultResponse.Success<BranchOffice>(existing);
    }
    
    public async Task<ResultResponse> Destroy(string code)
    {
        var existing = await context.BranchOffices.FindAsync(code);
        if (existing == null)
        {
            return new ResultResponse.Error("BranchOffice not found");
        }
        context.BranchOffices.Remove(existing);
        await context.SaveChangesAsync();
        
        return new ResultResponse.Success<string>("BranchOffice deleted");
    }
}