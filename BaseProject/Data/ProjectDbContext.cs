using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseProject.Models.Data;
using BaseProject.Models.Emuns;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
        ArgumentNullException.ThrowIfNull(modelBuilder);
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .Property(u => u.Role)
            .HasConversion(new EnumToStringConverter<Role>());
    }

}