using Azure.Storage.Blobs;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Configuration;

public static class ServiceColletionExtensions
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddDatabaseInfrastracture(configuration);

        return services;
    }

    public static IServiceCollection AddDatabaseInfrastracture(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var connectionString =
            configuration.GetConnectionString("PostGresConnectionString")
            ?? throw new InvalidOperationException(
                "Connection string 'PostGresConnectionString' not found."
            );

        services.AddDbContext<CoCreateDbContext>(options =>
            options.UseNpgsql(
                connectionString,
                x =>
                    x.UseNetTopologySuite()
                        .UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
            )
        );

        services.AddSingleton(sp => new BlobServiceClient(
            sp.GetRequiredService<IConfiguration>()
                .GetConnectionString("AzureBlobContainerConnectionString")
        ));

        return services;
    }
}
