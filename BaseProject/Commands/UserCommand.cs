using BaseProject.Data;
using BaseProject.Models.Data;
using BaseProject.Models.Helpers;
using Figgle.Fonts;
using Microsoft.AspNetCore.Identity;

namespace BaseProject.Commands;

public static class UserCommand
{
    public static async Task RunAddUserCommandAsync(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();
        var context = serviceProvider.GetRequiredService<ProjectDbContext>();
        await CreateUserAsync(userManager, roleManager, context);
    }

#pragma warning disable CA1303
    private static async Task CreateUserAsync(
        UserManager<User> userManager,
        RoleManager<Role> roleManager,
        ProjectDbContext dbContext
    )
    {
        Console.WriteLine(FiggleFonts.Standard.Render("BaseProject"));
        Console.WriteLine("--- Creación de Nuevo Usuario ---");

        // --- Role Selection ---
        var roles = roleManager.Roles.ToList();
        Console.WriteLine("\nRoles disponibles:");
        for (int i = 0; i < roles.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {roles[i].Name} (Nivel: {roles[i].LevelAccess})");
        }
        Console.WriteLine($"{roles.Count + 1}. Crear nuevo rol");

        string roleName = "";
        Console.Write("Seleccione una opción o ingrese el nombre del rol: ");
        var roleInput = Console.ReadLine() ?? "";

        if (int.TryParse(roleInput, out int roleIndex) && roleIndex >= 1 && roleIndex <= roles.Count)
        {
            roleName = roles[roleIndex - 1].Name!;
        }
        else if (roleIndex == roles.Count + 1)
        {
            Console.Write("Nombre del nuevo rol: ");
            roleName = Console.ReadLine() ?? "User";
            Console.Write("Nivel de acceso (0-100): ");
            if (!int.TryParse(Console.ReadLine(), out int level))
            {
                level = 0;
            }

            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new Role { Name = roleName, LevelAccess = level });
                Console.WriteLine($"✅ Rol '{roleName}' creado.");
            }
        }
        else
        {
            roleName = string.IsNullOrWhiteSpace(roleInput) ? "User" : roleInput;
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                Console.WriteLine($"⚠️ El rol '{roleName}' no existe. Se creará con nivel 0.");
                await roleManager.CreateAsync(new Role { Name = roleName, LevelAccess = 0 });
            }
        }

        // --- User Data ---
        Console.Write("\nNombre de usuario: ");
        var username = Console.ReadLine() ?? "";
        Console.Write("Nombre: ");
        var name = Console.ReadLine() ?? "";
        Console.Write("Apellido: ");
        var lastName = Console.ReadLine() ?? "";

        // --- Password Validation Toggle ---
        Console.Write("¿Desea validar la contraseña según las reglas de Identity? (S/n, default S): ");
        var validateInput = Console.ReadLine()?.ToUpperInvariant();
        bool validatePassword = string.IsNullOrEmpty(validateInput) || validateInput == "S";

        // --- Password Loop ---
        string password = "";
        while (true)
        {
            Console.Write("Contraseña: ");
            password = ReadPassword();
            Console.Write("\nConfirme contraseña: ");
            var confirmPassword = ReadPassword();
            Console.WriteLine();

            if (password != confirmPassword)
            {
                Console.WriteLine("❌ Las contraseñas no coinciden. Intente de nuevo.");
                continue;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine("❌ La contraseña no puede estar vacía.");
                continue;
            }

            if (validatePassword)
            {
                var validators = userManager.PasswordValidators;
                var dummyUser = new User { UserName = username };
                bool allValid = true;
                foreach (var v in validators)
                {
                    var result = await v.ValidateAsync(userManager, dummyUser, password);
                    if (!result.Succeeded)
                    {
                        Console.WriteLine("❌ La contraseña no cumple con los requisitos:");
                        foreach (var error in result.Errors)
                        {
                            Console.WriteLine($"  - {error.Description}");
                        }
                        allValid = false;
                        break;
                    }
                }
                if (!allValid) continue;
            }

            break;
        }

        // --- Creation ---
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

            IdentityResult result;
            if (validatePassword)
            {
                result = await userManager.CreateAsync(user, password);
            }
            else
            {
                // Manual hashing if validation is bypassed
                user.PasswordHash = userManager.PasswordHasher.HashPassword(user, password);
                result = await userManager.CreateAsync(user);
            }

            EnsureSucceeded(result, $"❌ Error al crear el usuario: {username}");

            var roleAssignResult = await userManager.AddToRoleAsync(user, roleName);
            EnsureSucceeded(roleAssignResult, $"❌ Error al asignar rol al usuario: {username}");

            Console.WriteLine($"✅ Usuario '{username}' creado y asignado al rol '{roleName}' correctamente.");

        },
        e => Console.WriteLine($"❌ Error en la transacción:\n{e}")
        );
    }

    private static string ReadPassword()
    {
        var password = new System.Text.StringBuilder();
        while (true)
        {
            var key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Enter) break;
            if (key.Key == ConsoleKey.Backspace)
            {
                if (password.Length > 0)
                {
                    password.Remove(password.Length - 1, 1);
                    Console.Write("\b \b");
                }
            }
            else
            {
                password.Append(key.KeyChar);
                Console.Write("*");
            }
        }
        return password.ToString();
    }

#pragma warning restore CA1303

    private static void EnsureSucceeded(IdentityResult result, string contextMessage)
    {
        if (result.Succeeded) return;
        var errors = string.Join("\n", result.Errors.Select(e => $"- {e.Description}"));
        throw new InvalidOperationException($"{contextMessage} \nDetalles:\n{errors}");
    }
}
