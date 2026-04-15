using BaseProject.Data;
using BaseProject.Models.Contracts;
using BaseProject.Models.Contracts.Dtos;
using BaseProject.Models.Contracts.Responses;
using BaseProject.Models.Data;
using BaseProject.Models.Extensions;
using BaseProject.Models.Mappers;
using Microsoft.EntityFrameworkCore;

namespace BaseProject.Services.Data;

public class ProductService(ProjectDbContext context)
{
    public IOrderedQueryable<Product> GetAll(ProductFilter filter)
    {
        var query = context.Products.AsQueryable();
        var search = filter.Search;
        var lastSync = filter.LastSync;
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(x => x.Code.Contains(search) || x.Name.Contains(search) || x.Detail.Contains(search));
        }
        
        if (lastSync.HasValue)
        {
             query = query.Where(x => x.UpdatedAt > lastSync.Value);
        }

        return query.OrderBy(x => x.Code);
    }
    
    public async Task<ResultResponse> GetByCode(string code)
    {
        var product = await context.Products.FindAsync(code);
        if (product == null)
        {
            return new ResultResponse.Error("Product not found");
        }
        return new ResultResponse.Success<Product>(product);
    }
    
    public async Task<ResultResponse> Create(ProductDto dto)
    {
        if (await context.Products.AnyAsync(x => x.Code == dto.Code))
        {
            return new ResultResponse.Error($"Product {dto.Code} already exists");
        }

        var product = dto.ToEntity();
        
        await context.Products.AddAsync(product);
        await context.SaveChangesAsync();
        
        return new ResultResponse.Success<Product>(product);
    }
    
    public async Task<ResultResponse> Update(string code, ProductDto dto)
    {
        var existing = await context.Products.FindAsync(code);
        if (existing == null)
        {
            return new ResultResponse.Error("Product not found");
        }
        
        existing.Name = dto.Name;
        existing.Detail = dto.Detail;
        existing.Location = dto.Location;
        
        context.Products.Update(existing);
        await context.SaveChangesAsync();
        
        return new ResultResponse.Success<Product>(existing);
    }
    
    public async Task<ResultResponse> Delete(string code)
    {
        var existing = await context.Products.FindAsync(code);
        if (existing == null)
        {
            return new ResultResponse.Error("Product not found");
        }
        existing.Delete(existing.DeletedAt == null);
        await context.SaveChangesAsync();
        
        return new ResultResponse.Success<Product>(existing);
    }
    
    public async Task<ResultResponse> Destroy(string code)
    {
        var existing = await context.Products.FindAsync(code);
        if (existing == null)
        {
            return new ResultResponse.Error("Product not found");
        }
        context.Products.Remove(existing);
        await context.SaveChangesAsync();
        
        return new ResultResponse.Success<string>("Product deleted");
    }
}
