using System.Security.Claims;
using Application.Interfaces;

namespace API.Services;

public class CurrentUserContextService : ICurrentUserContextService
{
    private readonly IHttpContextAccessor httpContextAccessor;

    public CurrentUserContextService(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor;
    }

    public int GetUserId()
    {
        var userId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null)
        {
            throw new Exception("User id is null");
        }

        return int.Parse(userId);
    }

    public string GetEmail()
    {
        var email = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email);

        if (email is null)
        {
            throw new Exception("Email is null");
        }

        return email;
    }
}