using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

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
    public HttpClient AuthenticatedClient { get; private set; } = null!;
    public int BaseUserId { get; private set; }

    protected BaseIntegrationTest(TestingWebAppFactory factory)
    {
        Factory = factory;
        Scope = factory.Services.CreateScope();

        CoCreateDbContext = Scope.ServiceProvider.GetRequiredService<CoCreateDbContext>();
        Client = factory.CreateClient();
    }

    private static string GenerateJwtToken(int userId)
    {
        var key = TestingWebAppFactory.testJwtKey;
        var issuer = TestingWebAppFactory.testJwtIssuer;

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[] { new Claim(ClaimTypes.NameIdentifier, userId.ToString()) };

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: issuer,
            claims: claims,
            expires: DateTime.UtcNow.AddDays(90),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public HttpClient GetAuthenticatedClient(int userId)
    {
        var client = Factory.CreateClient();
        var token = GenerateJwtToken(userId);
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

        return client;
    }

    public async Task InitializeAsync()
    {
        await CoCreateDbContext.Database.EnsureDeletedAsync();
        await CoCreateDbContext.Database.MigrateAsync();

        var baseUser = TestDataHelper.BaseUser;
        await CoCreateDbContext.Users.AddAsync(baseUser);
        await CoCreateDbContext.SaveChangesAsync();

        BaseUserId = baseUser.Id;
        AuthenticatedClient = GetAuthenticatedClient(BaseUserId);
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
