using System.Security.Claims;
using Infrastructure.Interfaces;

namespace API.Services;

public class CurrentUser(IHttpContextAccessor httpContextAccessor) : ICurrentUser
{
    public int GetUserId()
    {
        var userId =
            (httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier))
            ?? throw new Exception("User id is null");

        if (!int.TryParse(userId, out var id))
        {
            throw new Exception("Invalid user id format");
        }

        return id;
    }

    public string GetEmail()
    {
        var email =
            (httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email))
            ?? throw new Exception("Email is null");
        return email;
    }
}
