using System.Security.Claims;
using Application.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace API.Services;

public class CurrentHubUser : ICurrentHubUser
{
    private static readonly AsyncLocal<HubCallerContext?> Context = new();
    public int UserId { get; private set; }

    public void SetContext(HubCallerContext context)
    {
        Context.Value = context;
        UserId = GetUserId();
    }

    public ClaimsPrincipal User =>
        Context.Value?.User ?? throw new InvalidOperationException("Hub context not set");

    public int GetUserId()
    {
        var userId =
            User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new InvalidOperationException("User id is null");

        if (!int.TryParse(userId, out var id))
        {
            throw new InvalidOperationException("Invalid user id format");
        }
        return id;
    }
}
