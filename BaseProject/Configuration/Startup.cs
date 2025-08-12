using Microsoft.OpenApi.Models;

namespace BaseProject.Configuration;

public class Startup(IConfiguration configuration)
{
    #region Config

    private IConfiguration Configuration { get; } = configuration;

    #endregion

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();

        #region swagger

        services.AddSwaggerGen(swagger =>
        {
            swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "BaseProject", Version = "v1" });
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

        #endregion
    }
}