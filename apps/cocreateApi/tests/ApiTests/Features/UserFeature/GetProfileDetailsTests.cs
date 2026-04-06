using System.Net;
using System.Net.Http.Json;
using API.Factories;
using ApiTests.Helpers;
using Application.Features.UserFeature.Common;
using Infrastructure.Entities;
using Infrastructure.Enums;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace ApiTests.Features.UserFeature;

public class GetProfileDetailsTests(TestingWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task GetProfileDetails_ReturnsUserProfileDetails()
    {
        // Act
        var resp = await AuthenticatedClient.GetAsync("/api/user/profile-details");

        // Assert
        resp.EnsureSuccessStatusCode();

        var getProfileDetailsResponse = await resp.Content.ReadFromJsonAsync<ProfileDetails>();
        Assert.NotNull(getProfileDetailsResponse);
        Assert.Equal(TestDataHelper.BaseUser.Username, getProfileDetailsResponse.Username);
        Assert.Equal(TestDataHelper.BaseUser.Email, getProfileDetailsResponse.Email);
    }

    [Fact]
    public async Task GetProfileDetails_ReturnsUserWithSkills()
    {
        // Arrange
        var skills = new List<Skill>
        {
            new()
            {
                SkillType = SkillType.Editor,
                SkillGroupType = SkillGroupType.Filmmaking,
                Keywords = ["editing", "post-production"],
                UserId = BaseUserId,
            },
            new()
            {
                SkillType = SkillType.Director,
                SkillGroupType = SkillGroupType.Filmmaking,
                Keywords = ["directing", "vision"],
                UserId = BaseUserId,
            },
        };

        await CoCreateDbContext.Skills.AddRangeAsync(skills);
        await CoCreateDbContext.SaveChangesAsync();

        // Act
        var resp = await AuthenticatedClient.GetAsync("/api/user/profile-details");

        // Assert
        resp.EnsureSuccessStatusCode();

        var getProfileDetailsResponse = await resp.Content.ReadFromJsonAsync<ProfileDetails>();
        Assert.NotNull(getProfileDetailsResponse);
        Assert.Equal(2, getProfileDetailsResponse.Skills.Count);
        Assert.Contains(getProfileDetailsResponse.Skills, s => s.SkillType == SkillType.Editor);
        Assert.Contains(getProfileDetailsResponse.Skills, s => s.SkillType == SkillType.Director);
    }

    [Fact]
    public async Task GetProfileDetails_ReturnsUserWithPortfolioMedias()
    {
        // Arrange
        var portfolioMedias = new List<PortfolioContentMedia>
        {
            new()
            {
                Uri = "https://example.com/image1.jpg",
                Order = 1,
                MediaType = MediaType.Image,
                UserId = BaseUserId,
            },
            new()
            {
                Uri = "https://example.com/video1.mp4",
                Order = 2,
                MediaType = MediaType.Video,
                UserId = BaseUserId,
            },
        };

        await CoCreateDbContext.PortfolioContentMedias.AddRangeAsync(portfolioMedias);
        await CoCreateDbContext.SaveChangesAsync();

        // Act
        var resp = await AuthenticatedClient.GetAsync("/api/user/profile-details");

        // Assert
        resp.EnsureSuccessStatusCode();

        var getProfileDetailsResponse = await resp.Content.ReadFromJsonAsync<ProfileDetails>();
        Assert.NotNull(getProfileDetailsResponse);
        Assert.Equal(2, getProfileDetailsResponse.PortfolioMedias.Count);
        Assert.Contains(
            getProfileDetailsResponse.PortfolioMedias,
            m => m.Uri == "https://example.com/image1.jpg" && m.MediaType == MediaType.Image
        );
        Assert.Contains(
            getProfileDetailsResponse.PortfolioMedias,
            m => m.Uri == "https://example.com/video1.mp4" && m.MediaType == MediaType.Video
        );
    }

    [Fact]
    public async Task GetProfileDetails_ReturnsPortfolioMediasInCorrectOrder()
    {
        // Arrange - Add portfolio medias in random order
        var portfolioMedias = new List<PortfolioContentMedia>
        {
            new()
            {
                Uri = "https://example.com/third.jpg",
                Order = 3,
                MediaType = MediaType.Image,
                UserId = BaseUserId,
            },
            new()
            {
                Uri = "https://example.com/first.jpg",
                Order = 1,
                MediaType = MediaType.Image,
                UserId = BaseUserId,
            },
            new()
            {
                Uri = "https://example.com/fifth.mp4",
                Order = 5,
                MediaType = MediaType.Video,
                UserId = BaseUserId,
            },
            new()
            {
                Uri = "https://example.com/second.jpg",
                Order = 2,
                MediaType = MediaType.Image,
                UserId = BaseUserId,
            },
            new()
            {
                Uri = "https://example.com/fourth.mp4",
                Order = 4,
                MediaType = MediaType.Video,
                UserId = BaseUserId,
            },
        };

        await CoCreateDbContext.PortfolioContentMedias.AddRangeAsync(portfolioMedias);
        await CoCreateDbContext.SaveChangesAsync();

        // Act
        var resp = await AuthenticatedClient.GetAsync("/api/user/profile-details");

        // Assert
        resp.EnsureSuccessStatusCode();

        var getProfileDetailsResponse = await resp.Content.ReadFromJsonAsync<ProfileDetails>();
        Assert.NotNull(getProfileDetailsResponse);
        Assert.Equal(5, getProfileDetailsResponse.PortfolioMedias.Count);

        // Verify the order
        Assert.Equal(
            "https://example.com/first.jpg",
            getProfileDetailsResponse.PortfolioMedias[0].Uri
        );
        Assert.Equal(
            "https://example.com/second.jpg",
            getProfileDetailsResponse.PortfolioMedias[1].Uri
        );
        Assert.Equal(
            "https://example.com/third.jpg",
            getProfileDetailsResponse.PortfolioMedias[2].Uri
        );
        Assert.Equal(
            "https://example.com/fourth.mp4",
            getProfileDetailsResponse.PortfolioMedias[3].Uri
        );
        Assert.Equal(
            "https://example.com/fifth.mp4",
            getProfileDetailsResponse.PortfolioMedias[4].Uri
        );
    }

    [Fact]
    public async Task GetProfileDetails_ReturnsCompleteUserProfile()
    {
        // Arrange
        var user = await CoCreateDbContext.Users.FirstOrDefaultAsync(u => u.Id == BaseUserId);
        Assert.NotNull(user);

        user.AboutYou = "I am a creative designer";
        user.Address = "123 Main St, City, Country";
        user.Location = new Point(-73.935242, 40.730610) { SRID = 4326 };
        user.ProfilePictureSrc = "https://example.com/profile.jpg";

        var skill = new Skill
        {
            SkillType = SkillType.Editor,
            SkillGroupType = SkillGroupType.Filmmaking,
            Keywords = ["editing"],
            UserId = user.Id,
        };

        var portfolioMedia = new PortfolioContentMedia
        {
            Uri = "https://example.com/portfolio1.jpg",
            Order = 1,
            MediaType = MediaType.Image,
            UserId = user.Id,
        };

        user.Skills.Add(skill);
        user.PortfolioMedias.Add(portfolioMedia);

        CoCreateDbContext.Users.Update(user);
        await CoCreateDbContext.SaveChangesAsync();

        // Act
        var resp = await AuthenticatedClient.GetAsync("/api/user/profile-details");

        // Assert
        resp.EnsureSuccessStatusCode();

        var getProfileDetailsResponse = await resp.Content.ReadFromJsonAsync<ProfileDetails>();
        Assert.NotNull(getProfileDetailsResponse);
        Assert.Equal(user.Username, getProfileDetailsResponse.Username);
        Assert.Equal(user.Email, getProfileDetailsResponse.Email);
        Assert.Equal(user.AboutYou, getProfileDetailsResponse.AboutYou);
        Assert.NotNull(getProfileDetailsResponse.Location);
        Assert.Equal(user.Address, getProfileDetailsResponse.Location.Address);
        Assert.Equal(user.Location.X, getProfileDetailsResponse.Location.Longitude);
        Assert.Equal(user.Location.Y, getProfileDetailsResponse.Location.Latitude);
        Assert.NotNull(getProfileDetailsResponse.ProfilePicture);
        Assert.Equal(user.ProfilePictureSrc, getProfileDetailsResponse.ProfilePicture.Uri);
        Assert.Equal(MediaType.Image, getProfileDetailsResponse.ProfilePicture.MediaType);
        Assert.Single(getProfileDetailsResponse.Skills);
        Assert.Single(getProfileDetailsResponse.PortfolioMedias);
    }

    [Fact]
    public async Task GetProfileDetails_ReturnsDefaultValues_WhenOptionalFieldsAreNull()
    {
        // Act
        var resp = await AuthenticatedClient.GetAsync("/api/user/profile-details");

        // Assert
        resp.EnsureSuccessStatusCode();

        var getProfileDetailsResponse = await resp.Content.ReadFromJsonAsync<ProfileDetails>();
        Assert.NotNull(getProfileDetailsResponse);
        Assert.Null(getProfileDetailsResponse.AboutYou);
        Assert.NotNull(getProfileDetailsResponse.Location);
        Assert.Equal(0, getProfileDetailsResponse.Location.Longitude);
        Assert.Equal(0, getProfileDetailsResponse.Location.Latitude);
        Assert.Equal("", getProfileDetailsResponse.Location.Address);
        Assert.NotNull(getProfileDetailsResponse.ProfilePicture);
        Assert.Equal("", getProfileDetailsResponse.ProfilePicture.Uri);
        Assert.Empty(getProfileDetailsResponse.Skills);
        Assert.Empty(getProfileDetailsResponse.PortfolioMedias);
    }

    [Fact]
    public async Task GetProfileDetails_Returns400_WhenUserIsNotFound()
    {
        // Arrange
        CoCreateDbContext.Users.RemoveRange(CoCreateDbContext.Users);
        await CoCreateDbContext.SaveChangesAsync();

        // Act
        var response = await AuthenticatedClient.GetAsync("/api/user/profile-details");

        // Assert
        Assert.False(response.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        var errorResponse = await response.Content.ReadFromJsonAsync<APIResponse<string>>();
        Assert.NotNull(errorResponse);
        Assert.Equal("error.user-not-found", errorResponse.ErrorCode);
        Assert.Equal("User not found", errorResponse.Error);
    }

    [Fact]
    public async Task GetProfileDetails_Returns401_WhenNotAuthenticated()
    {
        // Act
        var response = await Client.GetAsync("/api/user/profile-details");

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetProfileDetails_ReturnsCorrectUser_WhenMultipleUsersExist()
    {
        // Arrange - Create a second user
        var secondUser = new User
        {
            Id = 2,
            Username = "seconduser",
            Email = "seconduser@example.com",
            PasswordHash = "hashedpassword2",
            Coins = 200,
            AboutYou = "I am the second user",
            Address = "456 Second St, City, Country",
            Location = new Point(-74.0060, 40.7128) { SRID = 4326 },
            ProfilePictureSrc = "https://example.com/seconduser.jpg",
        };

        var secondUserSkill = new Skill
        {
            SkillType = SkillType.Director,
            SkillGroupType = SkillGroupType.Filmmaking,
            Keywords = ["directing", "cinematography"],
            UserId = secondUser.Id,
        };

        await CoCreateDbContext.Users.AddAsync(secondUser);
        await CoCreateDbContext.Skills.AddAsync(secondUserSkill);
        await CoCreateDbContext.SaveChangesAsync();

        // Create authenticated client for second user
        var secondUserClient = GetAuthenticatedClient(2);

        // Act
        var firstUserResp = await AuthenticatedClient.GetAsync("/api/user/profile-details");
        var secondUserResp = await secondUserClient.GetAsync("/api/user/profile-details");

        // Assert
        firstUserResp.EnsureSuccessStatusCode();
        secondUserResp.EnsureSuccessStatusCode();

        var firstUserProfile = await firstUserResp.Content.ReadFromJsonAsync<ProfileDetails>();
        var secondUserProfile = await secondUserResp.Content.ReadFromJsonAsync<ProfileDetails>();

        // Verify first user gets their own data
        Assert.NotNull(firstUserProfile);
        Assert.Equal(TestDataHelper.BaseUser.Username, firstUserProfile.Username);
        Assert.Equal(TestDataHelper.BaseUser.Email, firstUserProfile.Email);
        Assert.Empty(firstUserProfile.Skills);

        // Verify second user gets their own data
        Assert.NotNull(secondUserProfile);
        Assert.Equal(secondUser.Username, secondUserProfile.Username);
        Assert.Equal(secondUser.Email, secondUserProfile.Email);
        Assert.Equal(secondUser.AboutYou, secondUserProfile.AboutYou);
        Assert.NotNull(secondUserProfile.Location);
        Assert.Equal(secondUser.Address, secondUserProfile.Location.Address);
        Assert.Equal(secondUser.Location.X, secondUserProfile.Location.Longitude);
        Assert.Equal(secondUser.Location.Y, secondUserProfile.Location.Latitude);
        Assert.Single(secondUserProfile.Skills);
        Assert.Equal(SkillType.Director, secondUserProfile.Skills[0].SkillType);
    }
}
