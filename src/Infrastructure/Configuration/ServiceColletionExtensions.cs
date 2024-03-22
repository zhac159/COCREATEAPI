using Application.Interfaces;
using Azure.Storage.Blobs;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Configuration;

public static class ServiceColletionExtensions
{
    public static IServiceCollection AddDatabaseInfrastracture(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var connectionString = configuration.GetConnectionString("PostGresConnectionString");

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new Exception("Connection string is empty or null");
        }

        services.AddDbContext<CoCreateDbContext>(
            options => options.UseNpgsql(connectionString, x => x.UseNetTopologySuite())
        );

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAssetRepository, AssetRepository>();
        services.AddScoped<IPortofolioContentRepository, PortofolioContentRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IProjectRoleRepository, ProjectRoleRepostiory>();
        services.AddScoped<IEnquiryRepository, EnquiryRepository>();
        services.AddScoped<IEnquiryMessageRepository, EnquiryMessageRepository>();

        return services;
    }

    public static IServiceCollection AddAzureBlobStorageService(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var connectionString = configuration.GetConnectionString(
            "AzureBlobContainerConnectionString"
        );

        services.AddSingleton(
            x =>
                new BlobServiceClient(
                    connectionString
                        ?? throw new Exception("Azure Blob connection string is empty or null")
                )
        );

        return services;
    }

    public static IServiceCollection AddStorageService(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddAzureBlobStorageService(configuration);

        services.AddScoped<IStorageService, AzureBlobStorageService>();

        services.AddSingleton<IRedisService>(
            sp =>
                new RedisService(
                    configuration.GetConnectionString("RedisServer")
                        ?? throw new Exception("Redis connection string is empty or null")
                )
        );
        
        services.AddScoped<IMessageStorageService, RedisMessageStorage>();

        return services;
    }

    public static IServiceCollection AddSignalRService(this IServiceCollection services)
    {
        services.AddSignalR();
        services.AddScoped<IChatHubService, ChatHubService>();
        return services;
    }

    public static void ConfigureChatHub(this IApplicationBuilder app)
    {
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHub<ChatHubService>("/chatHub");
        });
    }
}
