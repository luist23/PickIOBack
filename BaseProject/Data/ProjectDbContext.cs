using BaseProject.Models;
using BaseProject.Models.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BaseProject.Data;

public sealed class ProjectDbContext : IdentityDbContext<User, Role, string>
{
    public ProjectDbContext(DbContextOptions options) : base(options)
    {
        Database.SetCommandTimeout(120);
    }
    
    public DbSet<UserSession> UserSessions { get; set; }

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
            .Where(e => e.Entity is TimeStampedModel && (e.State == EntityState.Added || e.State == EntityState.Modified));

        var now = long.Parse(DateTime.UtcNow.ToString("yyyyMMddHHmmss"));

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