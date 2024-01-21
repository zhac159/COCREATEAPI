using Application.Interfaces;
using Application.Services;
using Azure.Storage.Blobs;

namespace Application.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAssetService, AssetService>();
        return services;
    }

    public static IServiceCollection AddBlobStorageService(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var connectionString = configuration.GetConnectionString(
            "AzureBlobContainerConnectionString"
        );

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new Exception("Connection string is empty or null");
        }

        services.AddSingleton(x => new BlobServiceClient(connectionString));

        return services;
    }
}
