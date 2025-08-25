using BaseProject.Commands;
using BaseProject.Configuration;
using BaseProject.Migrations.Seeders;

internal class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var startup = new Startup(builder.Configuration);
        startup.ConfigureBasicServices(builder.Services);

        if (!await ExecuteCommands(args, builder).ConfigureAwait(false)) return;

        startup.ConfigureServices(builder.Services);
        var app = builder.Build();
        startup.ConfigureMiddlewares(app: app);

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        var summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        app.MapGet("/weatherforecast", () =>
            {
                var forecast = Enumerable.Range(1, 5).Select(index =>
                        new WeatherForecast
                        (
                            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                            Random.Shared.Next(-20, 55),
                            summaries[Random.Shared.Next(summaries.Length)]
                        ))
                    .ToArray();
                return forecast;
            })
            .WithName("GetWeatherForecast")
            .WithOpenApi();

        await app.RunAsync().ConfigureAwait(false);
    }

    #region Extras

    public static async Task<bool> ExecuteCommands(string[] args, WebApplicationBuilder builder)
    {
        if (args.Contains("add-admin"))
        {
            var serviceProvider = builder.Build().Services;
            await UserCommand.RunAddAdminCommandAsync(serviceProvider).ConfigureAwait(false);
            return false;
        }

        if (args.Contains("seeders"))
        {
            var serviceProvider = builder.Build().Services;
            await RoleSeeder.SeedAsync(serviceProvider).ConfigureAwait(false);
            return false;
        }

        return true;
    }

    #endregion
}

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}