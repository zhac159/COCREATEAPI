using System.Net.Http.Json;
using API.Factories;
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
        Assert.Equal(user.Username, loginResponse!.User.Username);
        Assert.Equal(user.Email, loginResponse.User.Email);
        Assert.Equal(user.Coins, loginResponse.User.Coins);
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

    [Fact]
    public async Task Login_WithNonExistentUsernameOrEmail_ReturnsBadRequest()
    {
        // Arrange
        var request = new { UsernameOrEmail = "nonexistentuser", Password = "password123" };

        // Act
        var resp = await Client.PostAsJsonAsync("/api/authentication/login", request);

        // Assert
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, resp.StatusCode);

        var errorResponse = await resp.Content.ReadFromJsonAsync<APIResponse<string>>();
        Assert.NotNull(errorResponse);
        Assert.Equal("error.user-not-found", errorResponse.ErrorCode);
        Assert.Equal("User or email not found", errorResponse.Error);
    }
}
