using Domain.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Configuration;

public static class ServiceColletionExtensions
{
    public static IServiceCollection AddDatabaseInfrastracture(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var GetConnectionString = configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrEmpty(GetConnectionString))
        {
            throw new Exception("Connection string is empty 1`");
        }


        services.AddDbContext<CoCreateDbContext>(options => options.UseNpgsql(GetConnectionString));

        services.AddScoped<ITestTableRepository, TestTableRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
