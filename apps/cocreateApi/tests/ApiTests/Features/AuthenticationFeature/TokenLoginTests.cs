using System.Net.Http.Headers;
using System.Net.Http.Json;
using ApiTests.Helpers;
using Application.Features.AuthenticationFeature.Common;
using Infrastructure.Entities;
using Infrastructure.Persistence;

namespace ApiTests.Features.AuthenticationFeature;

public class TokenLoginTests(TestingWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task TokenLogin_WithValidToken_ReturnsNewTokenAndUser()
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

        var loginRequest = new { UsernameOrEmail = "john", Password = "password123" };
        var loginResponse = await Client.PostAsJsonAsync("/api/authentication/login", loginRequest);
        loginResponse.EnsureSuccessStatusCode();

        var loginResult = await loginResponse.Content.ReadFromJsonAsync<LoginResponse>();
        Assert.NotNull(loginResult);

        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Bearer",
            loginResult.Token
        );

        // Act
        var tokenResponse = await Client.PostAsync("/api/authentication/token", null);

        // Assert
        tokenResponse.EnsureSuccessStatusCode();

        var tokenLoginResult = await tokenResponse.Content.ReadFromJsonAsync<LoginResponse>();
        Assert.NotNull(tokenLoginResult);
        Assert.Equal(user.Username, tokenLoginResult!.User.Username);
        Assert.Equal(user.Email, tokenLoginResult.User.Email);
        Assert.Equal(user.Coins, tokenLoginResult.User.Coins);
        Assert.False(string.IsNullOrWhiteSpace(tokenLoginResult.Token));
    }

    [Fact]
    public async Task TokenLogin_WithInvalidToken_ReturnsUnauthorized()
    {
        // Arrange
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Bearer",
            "invalid.jwt.token"
        );

        // Act
        var response = await Client.PostAsync("/api/authentication/token", null);

        // Assert
        Assert.False(response.IsSuccessStatusCode);
    }
}
