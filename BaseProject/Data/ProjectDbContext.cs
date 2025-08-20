using BaseProject.Models.Data;
using BaseProject.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace BaseProject.Data;

public class ProjectDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;

    public ProjectDbContext(DbContextOptions options) : base(options)
    {
        this.Database.SetCommandTimeout(120);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .Property(u => u.Role)
            .HasConversion(new EnumToStringConverter<Role>());
    }

}