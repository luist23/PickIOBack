using BaseProject.Data;
using BaseProject.Models.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace BaseProject.Configuration;

public class Startup
{
    #region Values
    private ProjectAppSettings AppSettings { get; } = new();
    private IConfiguration Configuration { get; }
    #endregion
    #region Initialization
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
        Configuration.GetSection(nameof(ProjectAppSettings)).Bind(AppSettings);
    }
    #endregion

    public void ConfigureServices(IServiceCollection services)
    {
        ConfigureBasicServices(services);
        services.AddEndpointsApiExplorer();
        ConfigureSwagger(services, name: "BaseProject", version: "1");
    }

    public void ConfigureBasicServices(IServiceCollection services)
    {
        Configuration.GetSection(nameof(ProjectAppSettings)).Bind(AppSettings);
        SetDbConnection(services, AppSettings);
        services
            .AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<ProjectDbContext>() // Tu DbContext con Identity
            .AddDefaultTokenProviders();
    }


    #region ConfigureSwagger
    private static void ConfigureSwagger(IServiceCollection services, string name, string version = "1")
    {
        services.AddSwaggerGen(swagger =>
        {
            swagger.SwaggerDoc("v1", new OpenApiInfo { Title = name, Version = version });
            swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme.",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            });
            swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
    }
    #endregion

    public static void SetDbConnection(IServiceCollection services, ProjectAppSettings appSettings)
    {
        services.AddDbContext<ProjectDbContext>(options => DbApplyOptions(options, appSettings.ConnectionStrings));
    }

    public static DbContextOptionsBuilder<ProjectDbContext> GetDbOption(ConnectionAppSettings connectionStrings)
    {
        var options = new DbContextOptionsBuilder<ProjectDbContext>();
        DbApplyOptions(options, connectionStrings);
        return options;
    }

    public static void DbApplyOptions(DbContextOptionsBuilder options, ConnectionAppSettings connectionStrings)
    {
        ArgumentNullException.ThrowIfNull(options);
        ArgumentNullException.ThrowIfNull(connectionStrings);
        switch (connectionStrings?.TypeConnection)
        {
            case "MySQL":
                options.UseMySql(connectionStrings.MySQL, ServerVersion.AutoDetect(connectionStrings.MySQL));
                break;
            case "SQLServer":
                options.UseSqlServer(connectionStrings.SQLServer, sqlServerOptions =>
                {
                    sqlServerOptions.CommandTimeout(120);
                });
                options.EnableSensitiveDataLogging();
                break;
            case "SQLite":
                options.UseSqlite(connectionStrings.SQLite);
                break;
            default:
                break;
        }
    }

}