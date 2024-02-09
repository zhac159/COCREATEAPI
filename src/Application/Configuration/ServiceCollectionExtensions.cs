using Application.Interfaces;
using Application.Services;

namespace Application.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAssetService, AssetService>();
        services.AddScoped<IPortofolioContentService, PortofolioContentService>();
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<IProjectRoleService, ProjectRoleService>();
        return services;
    }
}
