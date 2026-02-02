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
        var query = context.BarCodes.AsQueryable();
        var search = filter.Search;
        var lastSync = filter.LastSync;
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(x => x.Code.Contains(search) || x.InternalCode.Contains(search));
        }
        
        if (lastSync.HasValue)
        {
             query = query.Where(x => x.UpdateAt > lastSync.Value);
        }

        return query.OrderBy(x => x.Code);
    }
    
    public async Task<ResultResponse> GetByCode(string code)
    {
        var barcode = await context.BarCodes.FindAsync(code);
        if (barcode == null)
        {
            return new ResultResponse.Error("Barcode not found");
        }
        return new ResultResponse.Success<BarCode>(barcode);
    }
    
    public async Task<ResultResponse> Create(BarCodeDto barcodeDto)
    {
        if (await context.BarCodes.AnyAsync(x => x.Code == barcodeDto.Code))
        {
            return new ResultResponse.Error($"Barcode {barcodeDto.Code} already exists");
        }

        var barcode = barcodeDto.ToEntity();
        
        await context.BarCodes.AddAsync(barcode);
        await context.SaveChangesAsync();
        
        return new ResultResponse.Success<BarCode>(barcode);
    }
    
    public async Task<ResultResponse> Update(string code, BarCodeDto barcodeDto)
    {
        var existing = await context.BarCodes.FindAsync(code);
        if (existing == null)
        {
            return new ResultResponse.Error("Barcode not found");
        }
        
        existing.InternalCode = barcodeDto.InternalCode;
        
        context.BarCodes.Update(existing);
        await context.SaveChangesAsync();
        
        return new ResultResponse.Success<BarCode>(existing);
    }
    
    public async Task<ResultResponse> Delete(string code)
    {
        var existing = await context.BarCodes.FindAsync(code);
        if (existing == null)
        {
            return new ResultResponse.Error("Barcode not found");
        }
        existing.Delete(existing.DeleteAt == null);
        await context.SaveChangesAsync();
        
        return new ResultResponse.Success<string>("Barcode deleted");
    }
    
    public async Task<ResultResponse> Destroy(string code)
    {
        var existing = await context.BarCodes.FindAsync(code);
        if (existing == null)
        {
            return new ResultResponse.Error("Barcode not found");
        }
        context.BarCodes.Remove(existing);
        await context.SaveChangesAsync();
        
        return new ResultResponse.Success<string>("Barcode deleted");
    }
}