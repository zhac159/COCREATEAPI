using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.Interfaces;

public interface ICurrentHubUser
{
    void SetContext(HubCallerContext context);
    int GetUserId();
    int UserId { get; }
    ClaimsPrincipal User { get; }
}
