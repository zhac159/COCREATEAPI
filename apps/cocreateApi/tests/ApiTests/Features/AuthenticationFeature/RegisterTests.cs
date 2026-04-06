using System.Net;
using System.Net.Http.Json;
using ApiTests.Helpers;
using Application.Features.AuthenticationFeature.Common;

namespace ApiTests.Features.AuthenticationFeature;

public class RegisterTests(TestingWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task Register_WithValidData_ReturnsCreatedUserAndToken()
    {
        // Arrange
        var request = new
        {
            Username = "newuser",
            Email = "new@example.com",
            Password = "pass",
        };

        // Act
        var resp = await Client.PostAsJsonAsync("/api/authentication/register", request);

        // Assert
        resp.EnsureSuccessStatusCode();

        var registerResponse = await resp.Content.ReadFromJsonAsync<LoginResponse>();
        Assert.NotNull(registerResponse);
        Assert.Equal(request.Username, registerResponse!.User.Username);
        Assert.Equal(request.Email, registerResponse.User.Email);
        Assert.True(registerResponse.User.UserId > 0);
        Assert.False(string.IsNullOrWhiteSpace(registerResponse.Token));
    }

    [Fact]
    public async Task Register_WithDuplicateEmail_ReturnsBadRequest()
    {
        // Arrange
        var existingUser = new Infrastructure.Entities.User
        {
            Username = "existing",
            Email = "existingEmail@gmail.com",
            PasswordHash = "password",
            Coins = 0,
        };

        await CoCreateDbContext.Users.AddAsync(existingUser);
        await CoCreateDbContext.SaveChangesAsync();

        var request = new
        {
            Username = "newuser",
            Email = "existingEmail@gmail.com",
            Password = "pass",
        };

        // Act
        var resp = await Client.PostAsJsonAsync("/api/authentication/register", request);

        // Assert
        Assert.False(resp.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, resp.StatusCode);
    }

    [Fact]
    public async Task Register_WithDuplicateUsername_ReturnsBadRequest()
    {
        // Arrange
        var existingUser = new Infrastructure.Entities.User
        {
            Username = "existinguser",
            Email = "someemail@gmail.com",
            PasswordHash = "password",
            Coins = 0,
        };

        await CoCreateDbContext.Users.AddAsync(existingUser);
        await CoCreateDbContext.SaveChangesAsync();

        var request = new
        {
            Username = "existinguser",
            Email = "differentemail@gmail.com",
            Password = "pass",
        };

        // Act
        var resp = await Client.PostAsJsonAsync("/api/authentication/register", request);

        // Assert
        Assert.False(resp.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, resp.StatusCode);
    }
}
