using BaseProject.Models.Data;
using Microsoft.AspNetCore.Identity;

namespace BaseProject.Migrations.Seeders;

public static class RoleSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();
        var roles = new List<Role>
        {
            new() { Name = Role.SuperAdmin, LevelAccess = 100 },
            new() { Name = Role.Admin, LevelAccess = 50 },
            new() { Name = Role.User, LevelAccess = 1 }
        };

        foreach (var role in roles.Where(r => r.Name != null))
            if (!await roleManager.RoleExistsAsync(role.Name ?? "").ConfigureAwait(false))
                await roleManager.CreateAsync(role).ConfigureAwait(false);
    }

}
