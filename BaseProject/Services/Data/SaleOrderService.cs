using BaseProject.Data;
using BaseProject.Models.Contracts;
using BaseProject.Models.Contracts.Dtos;
using BaseProject.Models.Contracts.Responses;
using BaseProject.Models.Data;
using BaseProject.Models.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BaseProject.Services.Data;

public class SaleOrderService(ProjectDbContext context)
{
    public async Task<int> CountAsync(SaleOrderFilter filter)
    {
        return await GetAll(filter).CountAsync();
    }

    public IOrderedQueryable<SaleOrder> GetAll(SaleOrderFilter filter)
    {
        var query = context.SaleOrders.AsQueryable();
        var lastSync = filter.LastSync;
        var status = filter.Status;
        
        if (lastSync.HasValue)
        {
             query = query.Where(x => x.UpdatedAt > lastSync.Value);
        }
        
        if (status.HasValue)
        {
            query = query.Where(x => x.Status == status.Value);
        }

        return query.OrderBy(x => x.Id);
    }
    
    public async Task<ResultResponse> GetById(int id)
    {
        var saleOrder = await context.SaleOrders
            .Include(x => x.Products)
            .Include(x => x.ProductSerials)
            .FirstOrDefaultAsync(x => x.Id == id);
            
        if (saleOrder == null)
        {
            return new ResultResponse.Error("SaleOrder not found");
        }
        return new ResultResponse.Success<SaleOrder>(saleOrder);
    }
    
    public async Task<ResultResponse> Create(SaleOrderDto dto)
    {
        if (await context.SaleOrders.AnyAsync(x => x.Id == dto.Id))
        {
            return new ResultResponse.Error($"SaleOrder {dto.Id} already exists");
        }

        var saleOrder = dto.ToEntity();
        
        // Handle initial Status Updates if any provided (though unusual on create for logs, but possible)
        if (dto.StatusUpdates.Any())
        {
             await SaveStatusUpdates(dto.StatusUpdates);
        }

        await context.SaleOrders.AddAsync(saleOrder);
        await context.SaveChangesAsync();
        
        return new ResultResponse.Success<SaleOrder>(saleOrder);
    }
    
    public async Task<ResultResponse> Update(int id, SaleOrderDto dto)
    {
        var existing = await context.SaleOrders
            .Include(x => x.Products)
            .Include(x => x.ProductSerials)
            .FirstOrDefaultAsync(x => x.Id == id);
            
        if (existing == null)
        {
            return new ResultResponse.Error("SaleOrder not found");
        }
        
        // Update Order fields
        existing.CustomerCode = dto.CustomerCode;
        existing.Transference = dto.Transference;
        existing.TransferenceUser = dto.TransferenceUser;
        existing.Sync = dto.Sync;
        existing.SyncUser = dto.SyncUser;
        existing.UserId = dto.UserId;
        existing.Status = dto.Status;
        
        // Update Products
        UpdateProducts(existing, dto.Products);
        
        // Update Product Serials
        UpdateProductSerials(existing, dto.ProductSerials);
        
        // Handle Status Updates (Logs)
        if (dto.StatusUpdates.Any())
        {
            await SaveStatusUpdates(dto.StatusUpdates);
        }
        
        context.SaleOrders.Update(existing);
        await context.SaveChangesAsync();
        
        return new ResultResponse.Success<SaleOrder>(existing);
    }

    private void UpdateProducts(SaleOrder existing, List<SaleProductDto> incomingProducts)
    {
        // Identify products to remove
        var incomingProductCodes = incomingProducts.Select(p => p.ItemCode).ToHashSet();
        var productsToRemove = existing.Products.Where(p => !incomingProductCodes.Contains(p.ItemCode)).ToList();
        
        foreach (var productToRemove in productsToRemove)
        {
            existing.Products.Remove(productToRemove);
            context.SaleProducts.Remove(productToRemove);
        }
        
        // Identify products to add or update
        foreach (var productDto in incomingProducts)
        {
            var existingProduct = existing.Products.FirstOrDefault(p => p.ItemCode == productDto.ItemCode);
            if (existingProduct == null)
            {
                // New product
                existing.Products.Add(new SaleProduct
                {
                    SaleOrderId = existing.Id,
                    ItemCode = productDto.ItemCode,
                    AmountRequest = productDto.AmountRequest,
                    AmountDispatch = productDto.AmountDispatch,
                    Justify = productDto.Justify,
                    Adjustment = productDto.Adjustment,
                    TimeToExpire = productDto.TimeToExpire,
                    Type = productDto.Type
                });
            }
            else
            {
                // Update existing product
                existingProduct.AmountRequest = productDto.AmountRequest;
                existingProduct.AmountDispatch = productDto.AmountDispatch;
                existingProduct.Justify = productDto.Justify;
                existingProduct.Adjustment = productDto.Adjustment;
                existingProduct.TimeToExpire = productDto.TimeToExpire;
                existingProduct.Type = productDto.Type;
            }
        }
    }

    private void UpdateProductSerials(SaleOrder existing, List<SaleProductSerialDto> incomingSerials)
    {
        // Identify serials to remove (composite key ItemCode + Serial)
        // Simple approach: remove all and re-add? Or precise diff. 
        // Precise diff: Key is (SaleOrderId, ItemCode, Serial).
        
        var incomingKeys = incomingSerials.Select(s => $"{s.ItemCode}|{s.Serial}").ToHashSet();
        var serialsToRemove = existing.ProductSerials.Where(s => !incomingKeys.Contains($"{s.ItemCode}|{s.Serial}")).ToList();
        
        foreach (var serialToRemove in serialsToRemove)
        {
            existing.ProductSerials.Remove(serialToRemove);
            context.SaleProductSerials.Remove(serialToRemove);
        }
        
        foreach (var serialDto in incomingSerials)
        {
             // Check if exists
             if (!existing.ProductSerials.Any(s => s.ItemCode == serialDto.ItemCode && s.Serial == serialDto.Serial))
             {
                 existing.ProductSerials.Add(new SaleProductSerial
                 {
                     SaleOrderId = existing.Id,
                     ItemCode = serialDto.ItemCode,
                     Serial = serialDto.Serial
                 });
             }
             // No update logic needed for Serial entity usually as it's just keys, unless there are other fields. 
             // SaleProductSerial only has keys and Serial string.
        }
    }

    private async Task SaveStatusUpdates(List<SaleOrderStatusDto> statusUpdates)
    {
         foreach (var update in statusUpdates)
         {
             // Check if this specific status log already exists? 
             // Log usually is append-only. 
             // We might want to check duplicate by (IdOrder, Status, Time, UserId) or similar if needed, 
             // but usually logs are just added.
             
             // However, to be safe and avoid duplicates if client retries:
             // Assuming no unique capability other than all fields.
             // Maybe check if exists with same Time?
             
             var exists = await context.SaleOrderStatutes.AnyAsync(x => 
                 x.IdOrder == update.IdOrder && 
                 x.Status == update.Status && 
                 x.Time == update.Time && // DateTime comparison might be tricky with precision
                 x.UserId == update.UserId);
                 
             if (!exists)
             {
                 await context.SaleOrderStatutes.AddAsync(update.ToEntity());
             }
         }
    }
    
    public async Task<ResultResponse> Delete(int id)
    {
        var existing = await context.SaleOrders.FindAsync(id);
        if (existing == null)
        {
            return new ResultResponse.Error("SaleOrder not found");
        }
        existing.Delete(existing.DeletedAt == null);
        await context.SaveChangesAsync();
        
        return new ResultResponse.Success<SaleOrder>(existing);
    }
    
    public async Task<ResultResponse> Destroy(int id)
    {
        var existing = await context.SaleOrders.FindAsync(id);
        if (existing == null)
        {
            return new ResultResponse.Error("SaleOrder not found");
        }
        context.SaleOrders.Remove(existing);
        await context.SaveChangesAsync();
        
        return new ResultResponse.Success<string>("SaleOrder deleted");
    }
}
