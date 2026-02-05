using BaseProject.Data;
using BaseProject.Models.Contracts;
using BaseProject.Models.Contracts.Dtos;
using BaseProject.Models.Contracts.Responses;
using BaseProject.Models.Data;
using BaseProject.Models.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BaseProject.Services.Data;

public class PurchaseOrderService(ProjectDbContext context)
{
    public IOrderedQueryable<PurchaseOrder> GetAll(PurchaseOrderFilter filter)
    {
        var query = context.PurchaseOrders.AsQueryable();
        var lastSync = filter.LastSync;
        var status = filter.Status;
        
        if (lastSync.HasValue)
        {
             query = query.Where(x => x.UpdateAt > lastSync.Value);
        }
        
        if (status.HasValue)
        {
            query = query.Where(x => x.Status == status.Value);
        }

        return query.OrderBy(x => x.Id);
    }
    
    public async Task<ResultResponse> GetById(int id)
    {
        var purchaseOrder = await context.PurchaseOrders
            .Include(x => x.Products)
            .FirstOrDefaultAsync(x => x.Id == id);
            
        if (purchaseOrder == null)
        {
            return new ResultResponse.Error("PurchaseOrder not found");
        }
        return new ResultResponse.Success<PurchaseOrder>(purchaseOrder);
    }
    
    public async Task<ResultResponse> Create(PurchaseOrderDto dto)
    {
        if (await context.PurchaseOrders.AnyAsync(x => x.Id == dto.Id))
        {
            return new ResultResponse.Error($"PurchaseOrder {dto.Id} already exists");
        }

        var purchaseOrder = dto.ToEntity();
        
        await context.PurchaseOrders.AddAsync(purchaseOrder);
        await context.SaveChangesAsync();
        
        return new ResultResponse.Success<PurchaseOrder>(purchaseOrder);
    }
    
    public async Task<ResultResponse> Update(int id, PurchaseOrderDto dto)
    {
        var existing = await context.PurchaseOrders
            .Include(x => x.Products)
            .FirstOrDefaultAsync(x => x.Id == id);
            
        if (existing == null)
        {
            return new ResultResponse.Error("PurchaseOrder not found");
        }
        
        // Update Order fields
        existing.ProviderCode = dto.ProviderCode;
        existing.Invoice = dto.Invoice;
        existing.Transference = dto.Transference;
        existing.TransferenceUser = dto.TransferenceUser;
        existing.Sync = dto.Sync;
        existing.SyncUser = dto.SyncUser;
        existing.Status = dto.Status;
        existing.UserId = dto.UserId;
        
        // Update Products
        // Identify products to remove
        var incomingProductCodes = dto.Products.Select(p => p.ItemCode).ToHashSet();
        var productsToRemove = existing.Products.Where(p => !incomingProductCodes.Contains(p.ItemCode)).ToList();
        
        foreach (var productToRemove in productsToRemove)
        {
            existing.Products.Remove(productToRemove);
            context.PurchaseProducts.Remove(productToRemove);
        }
        
        // Identify products to add or update
        foreach (var productDto in dto.Products)
        {
            var existingProduct = existing.Products.FirstOrDefault(p => p.ItemCode == productDto.ItemCode);
            if (existingProduct == null)
            {
                // New product
                existing.Products.Add(new PurchaseProduct
                {
                    PurchaseOrderId = id,
                    ItemCode = productDto.ItemCode,
                    AmountRequest = productDto.AmountRequest,
                    AmountDispatch = productDto.AmountDispatch,
                    Justify = productDto.Justify,
                    Adjustment = productDto.Adjustment
                });
            }
            else
            {
                // Update existing product
                existingProduct.AmountRequest = productDto.AmountRequest;
                existingProduct.AmountDispatch = productDto.AmountDispatch;
                existingProduct.Justify = productDto.Justify;
                existingProduct.Adjustment = productDto.Adjustment;
            }
        }
        
        context.PurchaseOrders.Update(existing);
        await context.SaveChangesAsync();
        
        return new ResultResponse.Success<PurchaseOrder>(existing);
    }
    
    public async Task<ResultResponse> Delete(int id)
    {
        var existing = await context.PurchaseOrders.FindAsync(id);
        if (existing == null)
        {
            return new ResultResponse.Error("PurchaseOrder not found");
        }
        existing.Delete(existing.DeleteAt == null);
        await context.SaveChangesAsync();
        
        return new ResultResponse.Success<PurchaseOrder>(existing);
    }
    
    public async Task<ResultResponse> Destroy(int id)
    {
        var existing = await context.PurchaseOrders.FindAsync(id);
        if (existing == null)
        {
            return new ResultResponse.Error("PurchaseOrder not found");
        }
        context.PurchaseOrders.Remove(existing);
        await context.SaveChangesAsync();
        
        return new ResultResponse.Success<string>("PurchaseOrder deleted");
    }
}
