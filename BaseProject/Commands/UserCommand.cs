using BaseProject.Data;
using BaseProject.Models.Data;
using BaseProject.Models.Helpers;
using Figgle.Fonts;
using Microsoft.AspNetCore.Identity;

namespace BaseProject.Commands;

public static class UserCommand
{
    public static async Task RunAddAdminCommandAsync(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();
        var context = serviceProvider.GetRequiredService<ProjectDbContext>();
        await CreateSuperAdminAsync(userManager, roleManager, context);
    }

#pragma warning disable CA1303
    private static async Task CreateSuperAdminAsync(
        UserManager<User> userManager,
        RoleManager<Role> roleManager,
        ProjectDbContext dbContext
    )
    {
        Console.WriteLine(FiggleFonts.Standard.Render("BaseProyect"));
        Console.WriteLine("Creando un nuevo SuperAdmin...");
 
        var roleName = "SuperAdmin";
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new Role
            {
                Name = roleName,
                LevelAccess = 100
            });
        }

        Console.Write("Nombre de usuario: ");
        var username = Console.ReadLine() ?? "";
        Console.Write("Nombre: ");
        var name = Console.ReadLine() ?? "";
        Console.Write("Apellido: ");
        var lastName = Console.ReadLine() ?? "";
        Console.Write("Contraseña: ");
        var password = Console.ReadLine() ?? "";

        await TransactionHelper.ExecuteInTransactionAsync(dbContext, async () =>
        {

            var existingUser = await userManager.FindByNameAsync(username);
            if (existingUser != null)
            {
                Console.WriteLine("❌ El usuario ya existe.");
                return;
            }

            var user = new User
            {
                UserName = username,
                Name = name,
                LastName = lastName,
                Active = true
            };

            var result = await userManager.CreateAsync(user, password);
            EnsureSucceeded(result, $"❌ Error al crear el usuario: {username}");

            var roleAssignResult = await userManager.AddToRoleAsync(user, roleName);
            EnsureSucceeded(roleAssignResult, $"❌ Error al asignar rol al usuario: {username}");

            Console.WriteLine("✅ SuperAdmin creado y asignado al rol correctamente.");

        },
        e => Console.WriteLine($"❌ Error en la transacción:\n{e}")
        );


    }

#pragma warning restore CA1303


    private static void EnsureSucceeded(IdentityResult result, string contextMessage)
    {
        if (result.Succeeded) return;
        var errors = string.Join("\n", result.Errors.Select(e => $"- {e.Description}"));
        throw new InvalidOperationException($"{contextMessage} \nDetalles:\n{errors}");
    }

}