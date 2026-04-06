using System.Net;
using System.Net.Http.Json;
using ApiTests.Helpers;
using Application.Features.Common.Records;
using Infrastructure.Enums;
using Microsoft.EntityFrameworkCore;

namespace ApiTests.Features.ProjectFeature;

public class CreateProjectTests(TestingWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task CreateProject_WithValidData_ReturnsCreatedProject()
    {
        // Arrange
        var user = await CoCreateDbContext.Users.FindAsync(BaseUserId);
        Assert.NotNull(user);
        user.Coins = 2000;
        CoCreateDbContext.Users.Update(user);
        await CoCreateDbContext.SaveChangesAsync();

        var request = new
        {
            Project = new
            {
                Name = "Test Project",
                Description = "A test project",
                Date = DateTime.UtcNow.AddDays(1),
                Location = new
                {
                    Longitude = 10.0,
                    Latitude = 20.0,
                    Address = "123 Test St",
                },
                Medias = new[]
                {
                    new { Uri = "https://example.com/image1.jpg", MediaType = MediaType.Image },
                },
                ProjectRoles = new[]
                {
                    new
                    {
                        Name = "Actor",
                        Description = "Main actor",
                        Cost = 1000,
                        SkillType = SkillType.ActorModel,
                        Remote = false,
                    },
                },
            },
        };

        // Act
        var resp = await AuthenticatedClient.PostAsJsonAsync("/api/project", request);

        // Assert
        Assert.Equal(HttpStatusCode.OK, resp.StatusCode);

        var projectRecord = await resp.Content.ReadFromJsonAsync<ProjectRecord>();
        Assert.NotNull(projectRecord);
        Assert.Equal(request.Project.Name, projectRecord.Name);
        Assert.Equal(request.Project.Description, projectRecord.Description);
        Assert.True(projectRecord.Id > 0);

        // Check database
        var project = await CoCreateDbContext
            .Projects.Include(p => p.ProjectMedias)
            .Include(p => p.ProjectRoles)
            .FirstOrDefaultAsync(p => p.Id == projectRecord.Id);
        Assert.NotNull(project);
        Assert.Equal(BaseUserId, project.ProjectManagerId);
        Assert.Equal(request.Project.Location.Address, project.Address);
        Assert.Equal(request.Project.Location.Longitude, project.Location.X);
        Assert.Equal(request.Project.Location.Latitude, project.Location.Y);
        Assert.Single(project.ProjectMedias);
        Assert.Single(project.ProjectRoles);

        // Check coins were deducted
        await CoCreateDbContext.Entry(user).ReloadAsync();
        Assert.Equal(1000, user.Coins); // 2000 - 1000
    }

    [Fact]
    public async Task CreateProject_WithInsufficientCoins_ReturnsBadRequest()
    {
        // Arrange: Set user coins to less than required
        var user = await CoCreateDbContext.Users.FindAsync(BaseUserId);
        Assert.NotNull(user);
        user.Coins = 500; // Less than 1000 required
        await CoCreateDbContext.SaveChangesAsync();

        var request = new
        {
            Project = new
            {
                Name = "Expensive Project",
                Description = "A costly project",
                Date = DateTime.UtcNow.AddDays(1),
                Location = new
                {
                    Longitude = 10.0,
                    Latitude = 20.0,
                    Address = "123 Test St",
                },
                Medias = Array.Empty<object>(),
                ProjectRoles = new[]
                {
                    new
                    {
                        Name = "Actor",
                        Description = "Main actor",
                        Cost = 1000,
                        SkillType = SkillType.ActorModel,
                        Remote = false,
                    },
                },
            },
        };

        // Act
        var resp = await AuthenticatedClient.PostAsJsonAsync("/api/project", request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, resp.StatusCode);

        var errorResponse =
            await resp.Content.ReadFromJsonAsync<API.Factories.APIResponse<string>>();
        Assert.NotNull(errorResponse);
        Assert.Contains("coins", errorResponse.Error?.ToLower() ?? "");
    }
}
