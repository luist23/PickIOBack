using System.Text;
using BaseProject.Data;
using BaseProject.Middlewares;
using BaseProject.Models.Data;
using BaseProject.Services.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
// using Microsoft.OpenApi.Models;

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
        Configuration.Bind(AppSettings);
    }

    #endregion

    #region Configure Services

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        ConfigureSwagger(services, name: "BaseProject", version: "1");
        ConfigureDataServices(services: services);
        ConfigureAuthentication(services: services, appSettings: AppSettings);
        ConfigureAuthorization(services: services);
    }

    #endregion

    #region Configure BasicServices

    public void ConfigureBasicServices(IServiceCollection services)
    {
        services.AddSingleton(AppSettings);
        SetDbConnection(services, AppSettings);
        services
            .AddIdentity<User, Role>()
            .AddEntityFrameworkStores<ProjectDbContext>()
            .AddDefaultTokenProviders();
    }

    #endregion

    public static void ConfigureMiddlewares(WebApplication app)
    {
        app.UseMiddleware<SessionTokenMiddleware>();
    }

    private static void ConfigureDataServices(IServiceCollection services)
    {
        services.AddScoped<AuthService>();
    }

    #region ConfigureAuthentication

    private static void ConfigureAuthentication(IServiceCollection services, ProjectAppSettings appSettings)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = appSettings.Jwt.Issuer,
                ValidAudience = appSettings.Jwt.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(appSettings.Jwt.Key))
            };
        });
    }

    private static void ConfigureAuthorization(IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            foreach (var permission in Permissions.GetAll())
            {
                options.AddPolicy(permission, policy => policy.RequireClaim("Permission", permission));
            }
        });
    }

    #endregion

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

    #region Configure DB

    private static void SetDbConnection(IServiceCollection services, ProjectAppSettings appSettings)
    {
        services.AddDbContext<ProjectDbContext>(options => DbApplyOptions(options, appSettings.ConnectionStrings));
    }

    public static DbContextOptionsBuilder<ProjectDbContext> GetDbOption(ConnectionAppSettings connectionStrings)
    {
        var options = new DbContextOptionsBuilder<ProjectDbContext>();
        DbApplyOptions(options, connectionStrings);
        return options;
    }

    private static void DbApplyOptions(DbContextOptionsBuilder options, ConnectionAppSettings connectionStrings)
    {
        switch (connectionStrings.TypeConnection)
        {
            case "MySql":
                options.UseMySql(connectionStrings.MySql, ServerVersion.AutoDetect(connectionStrings.MySql));
                break;
            case "SqlServer":
                options.UseSqlServer(connectionStrings.SqlServer,
                    sqlServerOptions => { sqlServerOptions.CommandTimeout(120); });
                options.EnableSensitiveDataLogging();
                break;
            case "SqLite":
                options.UseSqlite(connectionStrings.SqLite);
                break;
            default:
                break;
        }
    }

    #endregion
}