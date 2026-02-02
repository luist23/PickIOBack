using BaseProject.Models;
using BaseProject.Models.Data;
using BaseProject.Models.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BaseProject.Data;

public sealed class ProjectDbContext : IdentityDbContext<User, Role, string>
{
    public ProjectDbContext(DbContextOptions options) : base(options)
    {
        Database.SetCommandTimeout(120);
    }

    public DbSet<Aisle> Aisles { get; set; }
    public DbSet<BarCode> BarCodes { get; set; }
    public DbSet<BranchOffice> BranchOffices { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Justification> Justifications { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Provider> Providers { get; set; }
    public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
    public DbSet<PurchaseProduct> PurchaseProducts { get; set; }
    public DbSet<SaleOrder> SaleOrders { get; set; }
    public DbSet<SaleOrderLog> SaleOrderLogs { get; set; }
    public DbSet<SaleOrderStatus> SaleOrderStatutes { get; set; }
    public DbSet<SaleProduct> SaleProducts { get; set; }
    public DbSet<SaleProductSerial> SaleProductSerials { get; set; }
    public DbSet<UserSession> UserSessions { get; set; }
    public DbSet<WareHouse> WareHouses { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<UserSession>()
            .HasIndex(s => s.Token)
            .IsUnique();

        builder.Entity<User>()
            .HasMany(u => u.Sessions)
            .WithOne(s => s.User)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    public override int SaveChanges()
    {
        UpdateTimestamps();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateTimestamps();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateTimestamps()
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is TimeStampedModel &&
                        (e.State == EntityState.Added || e.State == EntityState.Modified));

        var now = TimeUtil.GetTimeLong();

        foreach (var entry in entries)
        {
            var entity = (TimeStampedModel)entry.Entity;

            if (entry.State == EntityState.Added)
            {
                entity.CreateAt = now;
            }

            entity.UpdateAt = now;
        }
    }
}