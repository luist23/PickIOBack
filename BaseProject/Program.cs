using BaseProject.Commands;
using BaseProject.Configuration;
using BaseProject.Migrations.Seeders;

namespace BaseProject;

internal static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var startup = new Startup(builder.Configuration);
        startup.ConfigureBasicServices(builder.Services);

        if (!await ExecuteCommands(args, builder)) return;

        startup.ConfigureServices(builder.Services);
        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseCors();

        app.UseAuthentication();
        app.UseAuthorization();
        Startup.ConfigureMiddlewares(app: app);
        app.MapControllers();

        await app.RunAsync();
    }

    #region Extras

    private static async Task<bool> ExecuteCommands(string[] args, WebApplicationBuilder builder)
    {
        if (args.Contains("add-user"))
        {
            var serviceProvider = builder.Build().Services;
            await UserCommand.RunAddUserCommandAsync(serviceProvider);
            return false;
        }

        if (args.Contains("seeders"))
        {
            var serviceProvider = builder.Build().Services;
            await RoleSeeder.SeedAsync(serviceProvider);
            return false;
        }

        return true;
    }

    #endregion
}