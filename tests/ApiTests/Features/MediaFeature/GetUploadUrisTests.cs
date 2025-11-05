using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using ApiTests.Helpers;
using Application.Features.AuthenticationFeature.Common;
using Application.Features.MediaFeature.GetUploadUris;
using Infrastructure.Entities;
using Infrastructure.Enums;

namespace ApiTests.Features.MediaFeature;

public class GetUploadUrisTests(TestingWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task GetUploadUris_WithValidRequest_ReturnsUploadUris()
    {
        // Arrange
        var user = new User
        {
            Username = "testuser",
            PasswordHash = "password123",
            Email = "test@example.com",
            Coins = 10,
        };

        await CoCreateDbContext.Users.AddAsync(user);
        await CoCreateDbContext.SaveChangesAsync();

        var token = await LoginAndGetToken("testuser", "password123");
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var request = new GetUploadUrisRequest
        {
            MediaTypes = new List<MediaType> { MediaType.Image, MediaType.Video },
            MediaCategory = MediaCategory.Portfolio,
        };

        // Act
        var response = await Client.PostAsJsonAsync("/api/media/get-upload-uris", request);

        // Assert
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<List<GetUploadUrisResponse>>();
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);

        var imageUri = result.FirstOrDefault(r => r.MediaType == MediaType.Image);
        Assert.NotNull(imageUri);
        Assert.False(string.IsNullOrWhiteSpace(imageUri.UploadUri));
        Assert.Contains("portfolio", imageUri.UploadUri.ToLowerInvariant());

        var videoUri = result.FirstOrDefault(r => r.MediaType == MediaType.Video);
        Assert.NotNull(videoUri);
        Assert.False(string.IsNullOrWhiteSpace(videoUri.UploadUri));
        Assert.Contains("portfolio", videoUri.UploadUri.ToLowerInvariant());
    }

    [Fact]
    public async Task GetUploadUris_WithSingleMediaType_ReturnsSingleUri()
    {
        // Arrange
        var user = new User
        {
            Username = "singleuser",
            PasswordHash = "password123",
            Email = "single@example.com",
            Coins = 10,
        };

        await CoCreateDbContext.Users.AddAsync(user);
        await CoCreateDbContext.SaveChangesAsync();

        var token = await LoginAndGetToken("singleuser", "password123");
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var request = new GetUploadUrisRequest
        {
            MediaTypes = new List<MediaType> { MediaType.Document },
            MediaCategory = MediaCategory.Portfolio,
        };

        // Act
        var response = await Client.PostAsJsonAsync("/api/media/get-upload-uris", request);

        // Assert
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<List<GetUploadUrisResponse>>();
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(MediaType.Document, result[0].MediaType);
        Assert.False(string.IsNullOrWhiteSpace(result[0].UploadUri));
    }

    [Fact]
    public async Task GetUploadUris_WithAllMediaTypes_ReturnsAllUris()
    {
        // Arrange
        var user = new User
        {
            Username = "alluser",
            PasswordHash = "password123",
            Email = "all@example.com",
            Coins = 10,
        };

        await CoCreateDbContext.Users.AddAsync(user);
        await CoCreateDbContext.SaveChangesAsync();

        var token = await LoginAndGetToken("alluser", "password123");
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var request = new GetUploadUrisRequest
        {
            MediaTypes = new List<MediaType>
            {
                MediaType.Image,
                MediaType.Video,
                MediaType.Audio,
                MediaType.Document,
            },
            MediaCategory = MediaCategory.Portfolio,
        };

        // Act
        var response = await Client.PostAsJsonAsync("/api/media/get-upload-uris", request);

        // Assert
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<List<GetUploadUrisResponse>>();
        Assert.NotNull(result);
        Assert.Equal(4, result.Count);

        foreach (var mediaType in request.MediaTypes)
        {
            var uri = result.FirstOrDefault(r => r.MediaType == mediaType);
            Assert.NotNull(uri);
            Assert.False(string.IsNullOrWhiteSpace(uri.UploadUri));
        }
    }

    [Fact]
    public async Task GetUploadUris_WithoutAuthentication_ReturnsUnauthorized()
    {
        // Arrange
        var request = new GetUploadUrisRequest
        {
            MediaTypes = new List<MediaType> { MediaType.Image },
            MediaCategory = MediaCategory.Portfolio,
        };

        // Act
        var response = await Client.PostAsJsonAsync("/api/media/get-upload-uris", request);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetUploadUris_WithInvalidToken_ReturnsUnauthorized()
    {
        // Arrange
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Bearer",
            "invalid.jwt.token"
        );

        var request = new GetUploadUrisRequest
        {
            MediaTypes = new List<MediaType> { MediaType.Image },
            MediaCategory = MediaCategory.Portfolio,
        };

        // Act
        var response = await Client.PostAsJsonAsync("/api/media/get-upload-uris", request);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetUploadUris_UrisContainSasToken()
    {
        // Arrange
        var user = new User
        {
            Username = "sasuser",
            PasswordHash = "password123",
            Email = "sas@example.com",
            Coins = 10,
        };

        await CoCreateDbContext.Users.AddAsync(user);
        await CoCreateDbContext.SaveChangesAsync();

        var token = await LoginAndGetToken("sasuser", "password123");
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var request = new GetUploadUrisRequest
        {
            MediaTypes = new List<MediaType> { MediaType.Image },
            MediaCategory = MediaCategory.Portfolio,
        };

        // Act
        var response = await Client.PostAsJsonAsync("/api/media/get-upload-uris", request);

        // Assert
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<List<GetUploadUrisResponse>>();
        Assert.NotNull(result);
        Assert.Single(result);

        // SAS URI should contain query parameters like sig, se, sp, etc.
        var uri = result[0].UploadUri;
        Assert.Contains("?", uri);
        Assert.Contains("sig=", uri);
        Assert.Contains("se=", uri);
        Assert.Contains("sp=", uri);
    }

    private async Task<string> LoginAndGetToken(string username, string password)
    {
        var loginRequest = new { UsernameOrEmail = username, Password = password };
        var loginResponse = await Client.PostAsJsonAsync("/api/authentication/login", loginRequest);
        loginResponse.EnsureSuccessStatusCode();

        var loginResult = await loginResponse.Content.ReadFromJsonAsync<LoginResponse>();
        Assert.NotNull(loginResult);
        Assert.False(string.IsNullOrWhiteSpace(loginResult.Token));

        return loginResult.Token;
    }
}
