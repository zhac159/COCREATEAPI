using Infrastructure.Interfaces;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Testcontainers.PostgreSql;

namespace ApiTests.Helpers;

public class TestingWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer dbContainer;
    public static readonly string testJwtKey = "YourSecretKeyForAuthenticationOfApplication";
    public static readonly string testJwtIssuer = "youtCompanyIssuer.com";

    public TestingWebAppFactory()
    {
        var postGisImage = "postgis/postgis:16-3.4";
        dbContainer = new PostgreSqlBuilder()
            .WithImage(postGisImage)
            .WithDatabase("testdb")
            .WithUsername("postgres")
            .WithPassword("postgres")
            .Build();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureLogging(logging =>
        {
            logging.ClearProviders();
            logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.None);
            logging.AddFilter("Microsoft.EntityFrameworkCore", LogLevel.None);
            logging.AddFilter(
                "Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware",
                LogLevel.None
            );
            logging.AddFilter("API.Filters.GlobalExceptionHandler", LogLevel.None);
            logging.SetMinimumLevel(LogLevel.Critical);
        });

        builder.ConfigureAppConfiguration(
            (context, config) =>
            {
                var settings = new Dictionary<string, string?>
                {
                    ["ConnectionStrings:PostGresConnectionString"] =
                        dbContainer.GetConnectionString(),
                    ["Jwt:Key"] = testJwtKey,
                    ["Jwt:Issuer"] = testJwtIssuer,
                };
                config.AddInMemoryCollection(settings!);
            }
        );

        builder.ConfigureServices(services =>
        {
            services.RemoveAll<DbContextOptions<CoCreateDbContext>>();

            services.AddDbContext<CoCreateDbContext>(options =>
            {
                options.UseNpgsql(
                    dbContainer.GetConnectionString(),
                    npgsqlOptions =>
                        npgsqlOptions
                            .UseNetTopologySuite()
                            .UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
                );
            });

            // Stub out storage deletes so tests don't require Azurite to be running.
            services.RemoveAll<IStorageService>();
            services.AddScoped<IStorageService, TestStorageService>();
        });
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await dbContainer.DisposeAsync();
    }

    public async Task InitializeAsync()
    {
        await dbContainer.StartAsync();
    }
}
