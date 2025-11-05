using System.Net;
using System.Net.Http.Json;
using API.Factories;
using ApiTests.Helpers;
using Application.Features.Common.Records;
using Application.Features.UserFeature.Common;
using Infrastructure.Entities;
using Infrastructure.Enums;
using Microsoft.EntityFrameworkCore;

namespace ApiTests.Features.UserFeature;

public class PutProfileDetailsTests(TestingWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task PutProfileDetails_UpdatesBasicUserInformation()
    {
        // Arrange
        var updateRequest = new UpdateProfileDetails
        {
            Username = "updateduser",
            Email = "updated@example.com",
            AboutYou = "Updated about me section",
            Location = new LocationRecord
            {
                Longitude = -73.935242,
                Latitude = 40.730610,
                Address = "New York, NY",
            },
            ProfilePicture = new MediaRecord
            {
                Uri = "https://example.com/newprofile.jpg",
                MediaType = MediaType.Image,
            },
            Skills = [],
            PortfolioMedias = [],
        };

        // Act
        var response = await AuthenticatedClient.PutAsJsonAsync(
            "/api/user/profile-details",
            updateRequest
        );

        // Assert
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<ProfileDetails>();
        Assert.NotNull(result);
        Assert.Equal("updateduser", result.Username);
        Assert.Equal("updated@example.com", result.Email);
        Assert.Equal("Updated about me section", result.AboutYou);
        Assert.NotNull(result.Location);
        Assert.Equal(-73.935242, result.Location.Longitude);
        Assert.Equal(40.730610, result.Location.Latitude);
        Assert.Equal("New York, NY", result.Location.Address);
        Assert.Equal("https://example.com/newprofile.jpg", result.ProfilePicture.Uri);

        // Verify database state
        var user = await CoCreateDbContext.Users.FindAsync(BaseUserId);
        Assert.NotNull(user);
        Assert.Equal("updateduser", user.Username);
        Assert.Equal("updated@example.com", user.Email);
        Assert.Equal("Updated about me section", user.AboutYou);
        Assert.Equal("https://example.com/newprofile.jpg", user.ProfilePictureSrc);
    }

    [Fact]
    public async Task PutProfileDetails_AddsNewSkills()
    {
        // Arrange
        var updateRequest = new UpdateProfileDetails
        {
            Username = TestDataHelper.BaseUser.Username,
            Email = TestDataHelper.BaseUser.Email,
            Location = null,
            ProfilePicture = new MediaRecord { Uri = "", MediaType = MediaType.Image },
            Skills =
            [
                new UpdateSkillRecord
                {
                    Id = null,
                    SkillType = SkillType.Editor,
                    SkillGroupType = SkillGroupType.Filmmaking,
                    Keywords = ["editing", "post-production"],
                },
                new UpdateSkillRecord
                {
                    Id = null,
                    SkillType = SkillType.Director,
                    SkillGroupType = SkillGroupType.Filmmaking,
                    Keywords = ["directing", "vision"],
                },
            ],
            PortfolioMedias = [],
        };

        // Act
        var response = await AuthenticatedClient.PutAsJsonAsync(
            "/api/user/profile-details",
            updateRequest
        );

        // Assert
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<ProfileDetails>();
        Assert.NotNull(result);
        Assert.Equal(2, result.Skills.Count);
        Assert.Contains(result.Skills, s => s.SkillType == SkillType.Editor);
        Assert.Contains(result.Skills, s => s.SkillType == SkillType.Director);

        // Verify database state
        var skills = await CoCreateDbContext
            .Skills.Where(s => s.UserId == BaseUserId)
            .ToListAsync();
        Assert.Equal(2, skills.Count);
    }

    [Fact]
    public async Task PutProfileDetails_UpdatesExistingSkills()
    {
        // Arrange - Create initial skill
        var existingSkill = new Skill
        {
            SkillType = SkillType.Editor,
            SkillGroupType = SkillGroupType.Filmmaking,
            Keywords = ["editing"],
            UserId = BaseUserId,
        };
        await CoCreateDbContext.Skills.AddAsync(existingSkill);
        await CoCreateDbContext.SaveChangesAsync();

        var updateRequest = new UpdateProfileDetails
        {
            Username = TestDataHelper.BaseUser.Username,
            Email = TestDataHelper.BaseUser.Email,
            Location = null,
            ProfilePicture = new MediaRecord { Uri = "", MediaType = MediaType.Image },
            Skills =
            [
                new UpdateSkillRecord
                {
                    Id = existingSkill.Id,
                    SkillType = SkillType.Producer,
                    SkillGroupType = SkillGroupType.Filmmaking,
                    Keywords = ["producing", "management"],
                },
            ],
            PortfolioMedias = [],
        };

        // Act
        var response = await AuthenticatedClient.PutAsJsonAsync(
            "/api/user/profile-details",
            updateRequest
        );

        // Assert
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<ProfileDetails>();
        Assert.NotNull(result);
        Assert.Single(result.Skills);
        Assert.Equal(SkillType.Producer, result.Skills[0].SkillType);
        Assert.Contains("producing", result.Skills[0].Keywords);
        Assert.Contains("management", result.Skills[0].Keywords);

        var updatedSkill = await CoCreateDbContext.Skills.FirstOrDefaultAsync(s =>
            s.Id == existingSkill.Id
        );
        Assert.NotNull(updatedSkill);
        Assert.Equal(SkillType.Producer, updatedSkill.SkillType);
    }

    [Fact]
    public async Task PutProfileDetails_RemovesSkillsNotInRequest()
    {
        // Arrange - Create initial skills
        var skill1 = new Skill
        {
            SkillType = SkillType.Editor,
            SkillGroupType = SkillGroupType.Filmmaking,
            Keywords = ["editing"],
            UserId = BaseUserId,
        };
        var skill2 = new Skill
        {
            SkillType = SkillType.Director,
            SkillGroupType = SkillGroupType.Filmmaking,
            Keywords = ["directing"],
            UserId = BaseUserId,
        };
        await CoCreateDbContext.Skills.AddRangeAsync([skill1, skill2]);
        await CoCreateDbContext.SaveChangesAsync();

        var updateRequest = new UpdateProfileDetails
        {
            Username = TestDataHelper.BaseUser.Username,
            Email = TestDataHelper.BaseUser.Email,
            Location = null,
            ProfilePicture = new MediaRecord { Uri = "", MediaType = MediaType.Image },
            Skills =
            [
                new UpdateSkillRecord
                {
                    Id = skill1.Id,
                    SkillType = SkillType.Editor,
                    SkillGroupType = SkillGroupType.Filmmaking,
                    Keywords = ["editing"],
                },
            ],
            PortfolioMedias = [],
        };

        // Act
        var response = await AuthenticatedClient.PutAsJsonAsync(
            "/api/user/profile-details",
            updateRequest
        );

        // Assert
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<ProfileDetails>();
        Assert.NotNull(result);
        Assert.Single(result.Skills);
        Assert.Equal(SkillType.Editor, result.Skills[0].SkillType);

        // Verify database state
        var skills = await CoCreateDbContext
            .Skills.Where(s => s.UserId == BaseUserId)
            .ToListAsync();
        Assert.Single(skills);
        Assert.Null(await CoCreateDbContext.Skills.FirstOrDefaultAsync(s => s.Id == skill2.Id));
    }

    [Fact]
    public async Task PutProfileDetails_AddsNewPortfolioMedias()
    {
        // Arrange
        var updateRequest = new UpdateProfileDetails
        {
            Username = TestDataHelper.BaseUser.Username,
            Email = TestDataHelper.BaseUser.Email,
            Location = null,
            ProfilePicture = new MediaRecord { Uri = "", MediaType = MediaType.Image },
            Skills = [],
            PortfolioMedias =
            [
                new UpdateMediaRecord
                {
                    Id = null,
                    Uri = "https://example.com/video1.mp4",
                    MediaType = MediaType.Video,
                },
                new UpdateMediaRecord
                {
                    Id = null,
                    Uri = "https://example.com/image1.jpg",
                    MediaType = MediaType.Image,
                },
            ],
        };

        // Act
        var response = await AuthenticatedClient.PutAsJsonAsync(
            "/api/user/profile-details",
            updateRequest
        );

        // Assert
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<ProfileDetails>();
        Assert.NotNull(result);
        Assert.Equal(2, result.PortfolioMedias.Count);
        Assert.Contains(result.PortfolioMedias, m => m.MediaType == MediaType.Video);
        Assert.Contains(result.PortfolioMedias, m => m.MediaType == MediaType.Image);

        // Verify database state
        var medias = await CoCreateDbContext
            .PortflioContentMedias.Where(m => m.UserId == BaseUserId)
            .ToListAsync();
        Assert.Equal(2, medias.Count);
    }

    [Fact]
    public async Task PutProfileDetails_UpdatesExistingPortfolioMedias()
    {
        // Arrange - Create initial media
        var existingMedia = new PortfolioContentMedia
        {
            Uri = "https://example.com/old.mp4",
            MediaType = MediaType.Video,
            Order = 1,
            UserId = BaseUserId,
        };
        await CoCreateDbContext.PortflioContentMedias.AddAsync(existingMedia);
        await CoCreateDbContext.SaveChangesAsync();

        var updateRequest = new UpdateProfileDetails
        {
            Username = TestDataHelper.BaseUser.Username,
            Email = TestDataHelper.BaseUser.Email,
            Location = null,
            ProfilePicture = new MediaRecord { Uri = "", MediaType = MediaType.Image },
            Skills = [],
            PortfolioMedias =
            [
                new UpdateMediaRecord
                {
                    Id = existingMedia.Id,
                    Uri = "https://example.com/updated.mp4",
                    MediaType = MediaType.Image,
                },
            ],
        };

        // Act
        var response = await AuthenticatedClient.PutAsJsonAsync(
            "/api/user/profile-details",
            updateRequest
        );

        // Assert
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<ProfileDetails>();
        Assert.NotNull(result);
        Assert.Single(result.PortfolioMedias);
        Assert.Equal("https://example.com/updated.mp4", result.PortfolioMedias[0].Uri);
        Assert.Equal(MediaType.Image, result.PortfolioMedias[0].MediaType);

        // Verify database state
        var updatedMedia = await CoCreateDbContext.PortflioContentMedias.FirstOrDefaultAsync(m =>
            m.Id == existingMedia.Id
        );
        Assert.NotNull(updatedMedia);
        Assert.Equal("https://example.com/updated.mp4", updatedMedia.Uri);
        Assert.Equal(MediaType.Image, updatedMedia.MediaType);
    }

    [Fact]
    public async Task PutProfileDetails_RemovesPortfolioMediasNotInRequest()
    {
        // Arrange - Create initial medias
        var media1 = new PortfolioContentMedia
        {
            Uri = "https://example.com/video1.mp4",
            MediaType = MediaType.Video,
            Order = 1,
            UserId = BaseUserId,
        };
        var media2 = new PortfolioContentMedia
        {
            Uri = "https://example.com/video2.mp4",
            MediaType = MediaType.Video,
            Order = 2,
            UserId = BaseUserId,
        };
        await CoCreateDbContext.PortflioContentMedias.AddRangeAsync([media1, media2]);
        await CoCreateDbContext.SaveChangesAsync();

        var updateRequest = new UpdateProfileDetails
        {
            Username = TestDataHelper.BaseUser.Username,
            Email = TestDataHelper.BaseUser.Email,
            Location = null,
            ProfilePicture = new MediaRecord { Uri = "", MediaType = MediaType.Image },
            Skills = [],
            PortfolioMedias =
            [
                new UpdateMediaRecord
                {
                    Id = media1.Id,
                    Uri = "https://example.com/video1.mp4",
                    MediaType = MediaType.Video,
                },
            ],
        };

        // Act
        var response = await AuthenticatedClient.PutAsJsonAsync(
            "/api/user/profile-details",
            updateRequest
        );

        // Assert
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<ProfileDetails>();
        Assert.NotNull(result);
        Assert.Single(result.PortfolioMedias);

        // Verify database state
        var medias = await CoCreateDbContext
            .PortflioContentMedias.Where(m => m.UserId == BaseUserId)
            .ToListAsync();
        Assert.Single(medias);
        Assert.Null(
            await CoCreateDbContext.PortflioContentMedias.FirstOrDefaultAsync(m =>
                m.Id == media2.Id
            )
        );
    }

    [Fact]
    public async Task PutProfileDetails_HandlesComplexUpdateWithMultipleChanges()
    {
        // Arrange - Create initial data
        var existingSkill = new Skill
        {
            SkillType = SkillType.Editor,
            SkillGroupType = SkillGroupType.Filmmaking,
            Keywords = ["editing"],
            UserId = BaseUserId,
        };
        var skillToRemove = new Skill
        {
            SkillType = SkillType.Director,
            SkillGroupType = SkillGroupType.Filmmaking,
            Keywords = ["directing"],
            UserId = BaseUserId,
        };
        var existingMedia = new PortfolioContentMedia
        {
            Uri = "https://example.com/old.mp4",
            MediaType = MediaType.Video,
            Order = 1,
            UserId = BaseUserId,
        };
        var mediaToRemove = new PortfolioContentMedia
        {
            Uri = "https://example.com/remove.jpg",
            MediaType = MediaType.Image,
            Order = 2,
            UserId = BaseUserId,
        };

        await CoCreateDbContext.Skills.AddRangeAsync([existingSkill, skillToRemove]);
        await CoCreateDbContext.PortflioContentMedias.AddRangeAsync([existingMedia, mediaToRemove]);
        await CoCreateDbContext.SaveChangesAsync();

        var updateRequest = new UpdateProfileDetails
        {
            Username = "complexuser",
            Email = "complex@example.com",
            AboutYou = "Complex update",
            Location = new LocationRecord
            {
                Longitude = -118.243683,
                Latitude = 34.052235,
                Address = "Los Angeles, CA",
            },
            ProfilePicture = new MediaRecord
            {
                Uri = "https://example.com/newprofile.jpg",
                MediaType = MediaType.Image,
            },
            Skills =
            [
                // Update existing
                new UpdateSkillRecord
                {
                    Id = existingSkill.Id,
                    SkillType = SkillType.Producer,
                    SkillGroupType = SkillGroupType.Filmmaking,
                    Keywords = ["producing"],
                },
                // Add new
                new UpdateSkillRecord
                {
                    Id = null,
                    SkillType = SkillType.LeadActorScreen,
                    SkillGroupType = SkillGroupType.Acting,
                    Keywords = ["acting", "performance"],
                },
            ],
            PortfolioMedias =
            [
                // Update existing
                new UpdateMediaRecord
                {
                    Id = existingMedia.Id,
                    Uri = "https://example.com/updated.mp4",
                    MediaType = MediaType.Video,
                },
                // Add new
                new UpdateMediaRecord
                {
                    Id = null,
                    Uri = "https://example.com/new.jpg",
                    MediaType = MediaType.Image,
                },
            ],
        };

        // Act
        var response = await AuthenticatedClient.PutAsJsonAsync(
            "/api/user/profile-details",
            updateRequest
        );

        // Assert
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<ProfileDetails>();
        Assert.NotNull(result);

        // Verify basic info
        Assert.Equal("complexuser", result.Username);
        Assert.Equal("complex@example.com", result.Email);
        Assert.Equal("Complex update", result.AboutYou);

        // Verify skills
        Assert.Equal(2, result.Skills.Count);
        Assert.Contains(result.Skills, s => s.SkillType == SkillType.Producer);
        Assert.Contains(result.Skills, s => s.SkillType == SkillType.LeadActorScreen);
        Assert.DoesNotContain(result.Skills, s => s.SkillType == SkillType.Director);

        // Verify medias
        Assert.Equal(2, result.PortfolioMedias.Count);
        Assert.Contains(result.PortfolioMedias, m => m.Uri == "https://example.com/updated.mp4");
        Assert.Contains(result.PortfolioMedias, m => m.Uri == "https://example.com/new.jpg");

        // Verify database state
        var user = await CoCreateDbContext
            .Users.Include(u => u.Skills)
            .Include(u => u.PortfolioMedias)
            .FirstOrDefaultAsync(u => u.Id == BaseUserId);
        Assert.NotNull(user);
        Assert.Equal(2, user.Skills.Count);
        Assert.Equal(2, user.PortfolioMedias.Count);
    }

    [Fact]
    public async Task PutProfileDetails_Returns400_WhenUserIsNotFound()
    {
        // Arrange
        CoCreateDbContext.Users.RemoveRange(CoCreateDbContext.Users);
        await CoCreateDbContext.SaveChangesAsync();

        var updateRequest = new UpdateProfileDetails
        {
            Username = "test",
            Email = "test@example.com",
            Location = null,
            ProfilePicture = new MediaRecord { Uri = "", MediaType = MediaType.Image },
            Skills = [],
            PortfolioMedias = [],
        };

        // Act
        var response = await AuthenticatedClient.PutAsJsonAsync(
            "/api/user/profile-details",
            updateRequest
        );

        // Assert
        Assert.False(response.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        var errorResponse = await response.Content.ReadFromJsonAsync<APIResponse<string>>();
        Assert.NotNull(errorResponse);
        Assert.Equal("error.user-not-found", errorResponse.ErrorCode);
        Assert.Equal("User not found.", errorResponse.Error);
    }

    [Fact]
    public async Task PutProfileDetails_MaintainsOrderOfPortfolioMedias()
    {
        // Arrange
        var updateRequest = new UpdateProfileDetails
        {
            Username = TestDataHelper.BaseUser.Username,
            Email = TestDataHelper.BaseUser.Email,
            Location = null,
            ProfilePicture = new MediaRecord { Uri = "", MediaType = MediaType.Image },
            Skills = [],
            PortfolioMedias =
            [
                new UpdateMediaRecord
                {
                    Id = null,
                    Uri = "https://example.com/third.jpg",
                    MediaType = MediaType.Image,
                },
                new UpdateMediaRecord
                {
                    Id = null,
                    Uri = "https://example.com/first.jpg",
                    MediaType = MediaType.Image,
                },
                new UpdateMediaRecord
                {
                    Id = null,
                    Uri = "https://example.com/second.jpg",
                    MediaType = MediaType.Image,
                },
            ],
        };

        // Act
        var response = await AuthenticatedClient.PutAsJsonAsync(
            "/api/user/profile-details",
            updateRequest
        );

        // Assert
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<ProfileDetails>();
        Assert.NotNull(result);
        Assert.Equal(3, result.PortfolioMedias.Count);

        // Verify medias are returned in order
        Assert.Equal("https://example.com/first.jpg", result.PortfolioMedias[0].Uri);
        Assert.Equal("https://example.com/second.jpg", result.PortfolioMedias[1].Uri);
        Assert.Equal("https://example.com/third.jpg", result.PortfolioMedias[2].Uri);

        // Verify database state
        var medias = await CoCreateDbContext
            .PortflioContentMedias.Where(m => m.UserId == BaseUserId)
            .OrderBy(m => m.Order)
            .ToListAsync();
        Assert.Equal(3, medias.Count);
        Assert.Equal(0, medias[0].Order);
        Assert.Equal(1, medias[1].Order);
        Assert.Equal(2, medias[2].Order);
    }
}
