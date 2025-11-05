using API.Filters;
using API.Services;
using Infrastructure.Interfaces;

namespace API.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddScoped<ICurrentUser, CurrentUser>();
        services.AddScoped<ICurrentHubUser, CurrentHubUser>();
        services.AddSingleton<CurrentHubUserFilter>();
        services.AddSignalR();

        return services;
    }
}
