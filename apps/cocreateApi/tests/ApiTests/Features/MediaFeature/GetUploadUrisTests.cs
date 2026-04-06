using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using ApiTests.Helpers;
using Application.Features.MediaFeature.GetUploadUris;
using Infrastructure.Enums;

namespace ApiTests.Features.MediaFeature;

public class GetUploadUrisTests(TestingWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task GetUploadUris_ReturnsUploadUris_WhenValidRequest()
    {
        // Arrange
        var request = new GetUploadUrisRequest
        {
            MediaTypes = [MediaType.Image, MediaType.Video],
            MediaCategory = MediaCategory.Portfolio,
        };

        // Act
        var response = await AuthenticatedClient.PostAsJsonAsync(
            "/api/media/get-upload-uris",
            request
        );

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
    public async Task GetUploadUris_ReturnsSingleUri_WhenSingleMediaType()
    {
        // Arrange
        var request = new GetUploadUrisRequest
        {
            MediaTypes = [MediaType.Document],
            MediaCategory = MediaCategory.Portfolio,
        };

        // Act
        var response = await AuthenticatedClient.PostAsJsonAsync(
            "/api/media/get-upload-uris",
            request
        );

        // Assert
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<List<GetUploadUrisResponse>>();
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(MediaType.Document, result[0].MediaType);
        Assert.False(string.IsNullOrWhiteSpace(result[0].UploadUri));
    }

    [Fact]
    public async Task GetUploadUris_ReturnsAllUris_WhenAllMediaTypes()
    {
        // Arrange
        var request = new GetUploadUrisRequest
        {
            MediaTypes = [MediaType.Image, MediaType.Video, MediaType.Audio, MediaType.Document],
            MediaCategory = MediaCategory.Portfolio,
        };

        // Act
        var response = await AuthenticatedClient.PostAsJsonAsync(
            "/api/media/get-upload-uris",
            request
        );

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
    public async Task GetUploadUris_ReturnsUnauthorized_WhenNoAuthentication()
    {
        // Arrange
        var request = new GetUploadUrisRequest
        {
            MediaTypes = [MediaType.Image],
            MediaCategory = MediaCategory.Portfolio,
        };

        // Act
        var response = await Client.PostAsJsonAsync("/api/media/get-upload-uris", request);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetUploadUris_ReturnsUnauthorized_WhenInvalidToken()
    {
        // Arrange
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Bearer",
            "invalid.jwt.token"
        );

        var request = new GetUploadUrisRequest
        {
            MediaTypes = [MediaType.Image],
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
        var request = new GetUploadUrisRequest
        {
            MediaTypes = [MediaType.Image],
            MediaCategory = MediaCategory.Portfolio,
        };

        // Act
        var response = await AuthenticatedClient.PostAsJsonAsync(
            "/api/media/get-upload-uris",
            request
        );

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
}
