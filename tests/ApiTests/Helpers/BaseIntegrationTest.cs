using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ApiTests.Helpers;

public abstract class BaseIntegrationTest
    : IClassFixture<TestingWebAppFactory>,
        IDisposable,
        IAsyncLifetime
{
    protected readonly IServiceScope Scope;
    protected readonly TestingWebAppFactory Factory;
    public readonly CoCreateDbContext CoCreateDbContext;

    public readonly HttpClient Client;

    protected BaseIntegrationTest(TestingWebAppFactory factory)
    {
        Factory = factory;
        Scope = factory.Services.CreateScope();

        CoCreateDbContext = Scope.ServiceProvider.GetRequiredService<CoCreateDbContext>();
        Client = factory.CreateClient();
    }

    public async Task InitializeAsync()
    {
        await CoCreateDbContext.Database.EnsureDeletedAsync();
        await CoCreateDbContext.Database.MigrateAsync();
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        Scope.Dispose();
        CoCreateDbContext.Dispose();
    }
}
