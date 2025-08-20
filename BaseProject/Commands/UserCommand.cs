using BaseProject.Models.Data;
using BaseProject.Models.Enums;
using Figgle.Fonts;
using Microsoft.AspNetCore.Identity;

namespace BaseProject.Commands;

public class UserCommand(UserManager<User> userManager)
{
    public async Task RunAddAdminCommandAsync()
    {
        Console.WriteLine(FiggleFonts.Standard.Render("ControlTower"));
        Console.WriteLine("Creando un nuevo SuperAdmin...");

        Console.Write("Nombre de usuario: ");
        var username = Console.ReadLine();
        Console.Write("Nombre: ");
        var name = Console.ReadLine();
        Console.Write("Apellido: ");
        var lastName = Console.ReadLine();
        Console.Write("Contraseña: ");
        var password = Console.ReadLine();

        var user = new User
        {
            UserName = username ?? "",
            Name = name ?? "",
            LastName = lastName ?? "",
            Role = Role.SuperAdmin,
            Active = true
        };

        var result = await userManager.CreateAsync(user, password ?? "");
        if (result.Succeeded)
        {
            Console.WriteLine("✅ SuperAdmin creado exitosamente.");
        }
        else
        {
            Console.WriteLine("❌ Error al crear el SuperAdmin:");
            foreach (var error in result.Errors)
            {
                Console.WriteLine($"- {error.Description}");
            }
        }
    }
}