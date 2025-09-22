using System.Net.Http.Json;
using ApiTests.Helpers;
using Application.Features.Authentication.Common;
using Infrastructure.Entities;
using Infrastructure.Persistence;

namespace ApiTests.Features.Authentication;

public class LoginTests(TestingWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task Login_WithValidCredentials_ReturnsTokenAndUser()
    {
        // Arrange
        var user = new User
        {
            Username = "john",
            PasswordHash = "password123",
            Email = "john@example.com",
            Coins = 10,
        };

        await CoCreateDbContext.Users.AddAsync(user);
        await CoCreateDbContext.SaveChangesAsync();

        var request = new { UsernameOrEmail = "john", Password = "password123" };

        // Act
        var resp = await Client.PostAsJsonAsync("/api/authentication/login", request);

        // Assert
        resp.EnsureSuccessStatusCode();

        var loginResponse = await resp.Content.ReadFromJsonAsync<LoginResponse>();
        Assert.NotNull(loginResponse);
        Assert.Equal(user.Username, loginResponse!.Username);
        Assert.Equal(user.Email, loginResponse.Email);
        Assert.Equal(user.Coins, loginResponse.Coins);
        Assert.False(string.IsNullOrWhiteSpace(loginResponse.Token));
    }

    [Fact]
    public async Task Login_WithInvalidPassword_ReturnsUnauthorized()
    {
        // Arrange
        var user = new User
        {
            Username = "jane",
            PasswordHash = "correct",
            Email = "jane@example.com",
            Coins = 5,
        };

        await CoCreateDbContext.Users.AddAsync(user);
        await CoCreateDbContext.SaveChangesAsync();

        var request = new { UsernameOrEmail = "jane", Password = "wrong" };

        // Act
        var resp = await Client.PostAsJsonAsync("/api/authentication/login", request);

        // Assert
        Assert.False(resp.IsSuccessStatusCode);
    }
}
