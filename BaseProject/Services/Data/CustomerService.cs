using BaseProject.Data;
using BaseProject.Models.Contracts;
using BaseProject.Models.Contracts.Dtos;
using BaseProject.Models.Contracts.Responses;
using BaseProject.Models.Data;
using BaseProject.Models.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BaseProject.Services.Data;

public class CustomerService(ProjectDbContext context)
{
    public IOrderedQueryable<Customer> GetAll(CustomerFilter filter)
    {
        var query = context.Customers.AsQueryable();
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
        var customer = await context.Customers.FindAsync(code);
        if (customer == null)
        {
            return new ResultResponse.Error("Customer not found");
        }
        return new ResultResponse.Success<Customer>(customer);
    }
    
    public async Task<ResultResponse> Create(CustomerDto dto)
    {
        if (await context.Customers.AnyAsync(x => x.Code == dto.Code))
        {
            return new ResultResponse.Error($"Customer {dto.Code} already exists");
        }

        var customer = dto.ToEntity();
        
        await context.Customers.AddAsync(customer);
        await context.SaveChangesAsync();
        
        return new ResultResponse.Success<Customer>(customer);
    }
    
    public async Task<ResultResponse> Update(string code, CustomerDto dto)
    {
        var existing = await context.Customers.FindAsync(code);
        if (existing == null)
        {
            return new ResultResponse.Error("Customer not found");
        }
        
        existing.Name = dto.Name;
        
        context.Customers.Update(existing);
        await context.SaveChangesAsync();
        
        return new ResultResponse.Success<Customer>(existing);
    }
    
    public async Task<ResultResponse> Delete(string code)
    {
        var existing = await context.Customers.FindAsync(code);
        if (existing == null)
        {
            return new ResultResponse.Error("Customer not found");
        }
        existing.Delete(existing.DeleteAt == null);
        await context.SaveChangesAsync();
        
        return new ResultResponse.Success<Customer>(existing);
    }
    
    public async Task<ResultResponse> Destroy(string code)
    {
        var existing = await context.Customers.FindAsync(code);
        if (existing == null)
        {
            return new ResultResponse.Error("Customer not found");
        }
        context.Customers.Remove(existing);
        await context.SaveChangesAsync();
        
        return new ResultResponse.Success<string>("Customer deleted");
    }
}
