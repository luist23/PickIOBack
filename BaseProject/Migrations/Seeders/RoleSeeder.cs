using BaseProject.Models.Data;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using BaseProject.Configuration;

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
        {
            if (!await roleManager.RoleExistsAsync(role.Name!))
            {
                await roleManager.CreateAsync(role);
            }
            
            // Seed Claims
            var existingRole = await roleManager.FindByNameAsync(role.Name!);
            if (existingRole != null)
            {
                await SeedClaimsForRole(roleManager, existingRole);
            }
        }
    }

    private static async Task SeedClaimsForRole(RoleManager<Role> roleManager, Role role)
    {
        var claims = await roleManager.GetClaimsAsync(role);
        var permissions = new List<string>();

        if (role.Name == Role.SuperAdmin)
        {
            permissions.AddRange(Permissions.GetAll());
        }
        else if (role.Name == Role.Admin)
        {
            // Admin gets all Product and User permissions
            permissions.Add(Permissions.Products.Read);
            permissions.Add(Permissions.Products.Create);
            permissions.Add(Permissions.Products.Edit);
            permissions.Add(Permissions.Products.Delete);
            permissions.Add(Permissions.Users.Read);
            permissions.Add(Permissions.Users.Create);
            permissions.Add(Permissions.Users.Edit); // Admin can edit users
        }
        else if (role.Name == Role.User)
        {
            // User only reads products
            permissions.Add(Permissions.Products.Read);
        }

        foreach (var permission in permissions)
        {
            if (!claims.Any(c => c.Type == "Permission" && c.Value == permission))
            {
                await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
            }
        }
    }

}
