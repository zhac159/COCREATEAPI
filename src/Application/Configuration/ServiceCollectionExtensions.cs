using Application.Interfaces;
using Application.Services;
using AutoMapper;

namespace Application.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ITestTableService, TestTableService>();
        services.AddScoped<IUserService, UserService>();
        services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);
        return services;
    }
}
