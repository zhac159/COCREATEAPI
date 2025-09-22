using Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.PostgreSql;

namespace ApiTests.Helpers;

public class TestingWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer dbContainer;

    public TestingWebAppFactory()
    {
        var postGisImage = "postgis/postgis:16-3.4";
        dbContainer = new PostgreSqlBuilder()
            .WithImage(postGisImage)
            .WithDatabase("testdb")
            .WithUsername("postgres")
            .WithPassword("postgres")
            .Build();
        dbContainer.StartAsync().GetAwaiter().GetResult();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(
            (context, config) =>
            {
                var settings = new Dictionary<string, string?>
                {
                    ["ConnectionStrings:PostGresConnectionString"] =
                        dbContainer.GetConnectionString(),
                };
                config.AddInMemoryCollection(settings!);
            }
        );

        builder.ConfigureServices(services =>
        {
            services.RemoveAll<DbContextOptions<CoCreateDbContext>>();

            services.AddDbContext<CoCreateDbContext>(options =>
                options.UseNpgsql(
                    dbContainer.GetConnectionString(),
                    x =>
                        x.UseNetTopologySuite()
                            .UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
                )
            );
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
