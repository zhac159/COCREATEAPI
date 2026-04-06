using Application.Interfaces;
using Application.Pipeline;
using Application.Services;
using FluentValidation;
using Mediator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddValidatorsFromAssembly(typeof(ServiceCollectionExtensions).Assembly);
        services.AddMediator(options =>
        {
            options.Assemblies = [typeof(ServiceCollectionExtensions).Assembly];
            options.ServiceLifetime = ServiceLifetime.Scoped;
        });

        services.AddScoped<ICoinsService, CoinsService>();
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(MessageValidatorBehaviour<,>));

        return services;
    }
}
