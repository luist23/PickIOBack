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

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}