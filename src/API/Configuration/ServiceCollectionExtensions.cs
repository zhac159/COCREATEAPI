using API.Services;
using Application.Interfaces;

namespace API.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddScoped<ICurrentUser, CurrentUser>();

        return services;
    }
}
