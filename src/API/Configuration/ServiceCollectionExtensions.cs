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
        });

        return services;
    }
}
