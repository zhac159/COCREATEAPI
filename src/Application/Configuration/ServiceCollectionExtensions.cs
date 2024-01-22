using Application.Interfaces;
using Application.Services;

namespace Application.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAssetService, AssetService>();
        return services;
    }
}
