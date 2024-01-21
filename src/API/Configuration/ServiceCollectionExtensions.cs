using API.Filters;
using API.Services;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace API.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAPI(this IServiceCollection services)
    {
        services.AddScoped<ICurrentUserContextService, CurrentUserContextService>();

        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });
        
        services.AddControllers(options =>
        {
            options.Filters.Add<APIExceptitionFilter>();
            options.Filters.Add<ModelValidationFilter>();
        });
        return services;
    }

public static IServiceCollection AddCustomSwaggerGen(this IServiceCollection services)
{
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "CoCreateAPI", Version = "v1" });

        // Define the BearerAuth scheme
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = "bearer" // Must be lowercase
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                new string[] {}
            }
        });
    });

    return services;
}
}
