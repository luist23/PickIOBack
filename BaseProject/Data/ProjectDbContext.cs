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
}