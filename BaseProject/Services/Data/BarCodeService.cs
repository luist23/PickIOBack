using BaseProject.Data;
using BaseProject.Models.Contracts;
using BaseProject.Models.Contracts.Dtos;
using BaseProject.Models.Contracts.Responses;
using BaseProject.Models.Data;
using BaseProject.Models.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BaseProject.Services.Data;

public class BarCodeService(ProjectDbContext context)
{

    public IOrderedQueryable<BarCode> GetAll(BarCodeFilter filter)
    {
        var query = context.Set<BarCode>().AsQueryable();
        var search = filter.Search;
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(x => x.Code.Contains(search) || x.InternalCode.Contains(search));
        }
        

        if (filter.LastSync.HasValue)
        {
             query = query.Where(x => x.UpdateAt > filter.LastSync.Value);
        }

        return query.OrderBy(x => x.Code);
    }
    
    public async Task<ResultResponse> GetByCode(string code)
    {
        var barcode = await context.Set<BarCode>().FindAsync(code);
        if (barcode == null)
        {
            return new ResultResponse.Error("Barcode not found");
        }
        return new ResultResponse.Success<BarCode>(barcode);
    }
    
    public async Task<ResultResponse> Create(BarCodeDto barcodeDto)
    {
        if (await context.Set<BarCode>().AnyAsync(x => x.Code == barcodeDto.Code))
        {
            return new ResultResponse.Error($"Barcode {barcodeDto.Code} already exists");
        }

        var barcode = barcodeDto.ToEntity();
        // Timestamps handled by DbContext
        
        await context.Set<BarCode>().AddAsync(barcode);
        await context.SaveChangesAsync();
        
        return new ResultResponse.Success<BarCode>(barcode);
    }
    
    public async Task<ResultResponse> Update(string code, BarCodeDto barcodeDto)
    {
        var existing = await context.Set<BarCode>().FindAsync(code);
        if (existing == null)
        {
            return new ResultResponse.Error("Barcode not found");
        }
        
        existing.InternalCode = barcodeDto.InternalCode;
        // Timestamps handled by DbContext
        
        context.Set<BarCode>().Update(existing);
        await context.SaveChangesAsync();
        
        return new ResultResponse.Success<BarCode>(existing);
    }

    public async Task<ResultResponse> ManualUpdate(string code)
    {
        var existing = await context.Set<BarCode>().FindAsync(code);
        if (existing == null)
        {
            return new ResultResponse.Error("Barcode not found");
        }

        // Just marking as modified will trigger UpdateAt in DbContext, but if no properties changed, EF might ignore it.
        // So we explicitly set UpdateAt here to ensure it changes, although DbContext overrides it.
        // Actually, to ensure DbContext sees a change if we just want to "touch" it:
        existing.Update(); 
        
        context.Set<BarCode>().Update(existing);
        await context.SaveChangesAsync();

        return new ResultResponse.Success<BarCode>(existing);
    }
    
    public async Task<ResultResponse> Delete(string code)
    {
        var existing = await context.Set<BarCode>().FindAsync(code);
        if (existing == null)
        {
            return new ResultResponse.Error("Barcode not found");
        }
        
        context.Set<BarCode>().Remove(existing);
        await context.SaveChangesAsync();
        
        return new ResultResponse.Success<string>("Barcode deleted");
    }
}