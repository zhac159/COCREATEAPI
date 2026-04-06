using System.Net.Http.Json;
using API.Factories;
using ApiTests.Helpers;
using Application.Features.AuthenticationFeature.Common;

namespace ApiTests.Features.AuthenticationFeature;

public class LoginTests(TestingWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task Login_WithValidUsernameAndPassword_ReturnsTokenAndUser()
    {
        var request = new
        {
            UsernameOrEmail = TestDataHelper.BaseUser.Username,
            Password = TestDataHelper.BaseUser.PasswordHash,
        };

        // Act
        var resp = await Client.PostAsJsonAsync("/api/authentication/login", request);

        // Assert
        resp.EnsureSuccessStatusCode();

        var loginResponse = await resp.Content.ReadFromJsonAsync<LoginResponse>();
        Assert.NotNull(loginResponse);
        Assert.Equal(TestDataHelper.BaseUser.Username, loginResponse!.User.Username);
        Assert.Equal(TestDataHelper.BaseUser.Email, loginResponse.User.Email);
        Assert.Equal(TestDataHelper.BaseUser.Coins, loginResponse.User.Coins);
        Assert.False(string.IsNullOrWhiteSpace(loginResponse.Token));
    }

    [Fact]
    public async Task Login_WithValidEmailAndPassword_ReturnsTokenAndUser()
    {
        var request = new
        {
            UsernameOrEmail = TestDataHelper.BaseUser.Email,
            Password = TestDataHelper.BaseUser.PasswordHash,
        };

        // Act
        var resp = await Client.PostAsJsonAsync("/api/authentication/login", request);

        // Assert
        resp.EnsureSuccessStatusCode();

        var loginResponse = await resp.Content.ReadFromJsonAsync<LoginResponse>();
        Assert.NotNull(loginResponse);
        Assert.Equal(TestDataHelper.BaseUser.Username, loginResponse!.User.Username);
        Assert.Equal(TestDataHelper.BaseUser.Email, loginResponse.User.Email);
        Assert.Equal(TestDataHelper.BaseUser.Coins, loginResponse.User.Coins);
        Assert.False(string.IsNullOrWhiteSpace(loginResponse.Token));
    }

    [Fact]
    public async Task Login_WithInvalidPassword_ReturnsUnauthorized()
    {
        var request = new
        {
            UsernameOrEmail = TestDataHelper.BaseUser.Username,
            Password = "wrongpassword",
        };

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
        Assert.Equal("error.username-or-email-not-found", errorResponse.ErrorCode);
        Assert.Equal("User or email not found", errorResponse.Error);
    }
}
