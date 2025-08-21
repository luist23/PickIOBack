using BaseProject.Data;

namespace BaseProject.Models.Helpers;

#pragma warning disable CA2007
#pragma warning disable CA1062
public static class TransactionHelper
{
    public static async Task ExecuteInTransactionAsync(
        ProjectDbContext dbContext,
        Func<Task> operation,
        Action<string>? onFailure = null)
    {
        await using var transaction = await dbContext.Database.BeginTransactionAsync();
        try
        {
            await operation();
            await dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            onFailure?.Invoke(e.Message);
        }
    }


    public static async Task<T> ExecuteInTransactionAsync<T>(
        ProjectDbContext dbContext,
        Func<Task<T>> operation,
        Action<string>? onFailure = null)
    {
        await using var transaction = await dbContext.Database.BeginTransactionAsync();
        try
        {
            var result = await operation();
            await dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
            return result;
        }
        catch (Exception e)
        {
            onFailure?.Invoke(e.Message);
            await transaction.RollbackAsync();
            throw;
        }
    }
}
#pragma warning restore CA2007
#pragma warning restore CA1062