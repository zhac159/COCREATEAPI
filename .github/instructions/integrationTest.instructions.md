---
applyTo: "**/*tests.cs"
---

# Integration Testing Guidelines

## Project Context

This is a .NET 9 Web API project using Clean Architecture with the following structure:

- API layer (ASP.NET Core Web API)
- Application layer (business logic, CQRS with MediatR)
- Infrastructure layer (EF Core, external services)
- Tests layer (xUnit integration tests with TestContainers)

## Testing Guidelines

### Test Structure

- Use `BaseIntegrationTest` as the base class for all integration tests
- Each test class should inherit from `BaseIntegrationTest`
- Use `TestingWebAppFactory` for test web application setup
- Database is automatically created/cleaned up using TestContainers PostgreSQL

### Authentication in Tests

- Use `AuthenticatedClient` for tests requiring authentication (pre-configured with jwt for userid 1)
- Each test will have a base user with ID 1 already available found at `TestDataHelper.BaseUser`
- Use `GetAuthenticatedClient(userId)` for specific user authentication
- JWT tokens are automatically generated using test JWT settings from `TestingWebAppFactory`

### Database Setup

- Database is automatically migrated in `InitializeAsync()`
- Use `CoCreateDbContext` to seed test data or verify results
- Database is cleaned between tests

### HTTP Client Usage

- Use `Client` for unauthenticated requests
- Use `AuthenticatedClient` for authenticated requests

### Test Naming Convention

- Test methods should be descriptive: `Should_ReturnOk_When_ValidRequest`
- Use xUnit `[Fact]` for single test cases
- Use xUnit `[Theory]` with `[InlineData]` for parameterized tests

### Assertions

- Verify HTTP status codes, response content, and database state
- Test both success and error scenarios

### Example Test Structur

```csharp
public class GetProfileDetailsTests(TestingWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task GetProfileDetails_ReturnsUserProfileDetails()
    {
        // Act
        var resp = await AuthenticatedClient.GetAsync("/api/user/profile-details");

        // Assert
        resp.EnsureSuccessStatusCode();

        var getProfileDetailsResponse =
            await resp.Content.ReadFromJsonAsync<GetProfileDetailsResponse>();
        Assert.NotNull(getProfileDetailsResponse);
        Assert.Equal(TestDataHelper.BaseUser.Username, getProfileDetailsResponse.Username);
        Assert.Equal(TestDataHelper.BaseUser.Email, getProfileDetailsResponse.Email);
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
}
```

### Endpoint Details

The endpoint and handler being tested in the example above is as follows:

```csharp
public sealed class GetProfileDetailsRequestHandler(
    CoCreateDbContext coCreateDbContext,
    ICurrentUser currentUser
) : IQueryHandler<GetProfileDetailsRequest, GetProfileDetailsResponse>
{
    public async ValueTask<GetProfileDetailsResponse> Handle(
        GetProfileDetailsRequest query,
        CancellationToken cancellationToken
    )
    {
        var user =
            await coCreateDbContext
                .Users.Where(u => u.Id == currentUser.GetUserId())
                .Include(u => u.Skills)
                .Include(u => u.PortfolioMedias)
                .FirstOrDefaultAsync() ?? throw new UserNotFoundException("User not found");

        return new GetProfileDetailsResponse
        {
            Username = user.Username,
            Email = user.Email,
            AboutYou = user.AboutYou,
            Location = LocationRecord.FromUser(user),
            Skills = [.. user.Skills.Select(SkillRecord.FromSkill)],
            PortfolioMedias =
            [
                .. user.PortfolioMedias.Select(MediaRecord.FromPorfolioContentMedia),
            ],
            ProfilePicture = MediaRecord.FromUri(user.ProfilePictureSrc ?? "", MediaType.Image),
        };
    }
}
```

```csharp
public static class GetProfileDetailsEndpoint
{
    public static RouteGroupBuilder MapGetProfileDetailsEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet(
            "/profile-details",
            async (IMediator mediator) =>
            {
                var getprofiledetailsResponse = await mediator.Send(new GetProfileDetailsRequest());
                return Results.Ok(getprofiledetailsResponse);
            }
        );

        return group;
    }
}
```

