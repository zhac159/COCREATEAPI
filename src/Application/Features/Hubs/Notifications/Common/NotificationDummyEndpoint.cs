using Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

// We add this dummy endpoint to make sure Orval.ts picks up the NotificationClasses used in the NotificationHub
namespace Application.Features.Hubs.Notifications.Common;

public static class LoginEndpoint
{
    public static RouteGroupBuilder MapNotificationDummyEndpoint(this RouteGroupBuilder group)
    {
        group
            .MapPost(
                "/notification-dummy-endpoint",
                async (IMediator mediator, NotificationClasses sendMessageRequest) =>
                {
                    return Results.Ok(sendMessageRequest);
                }
            )
            .WithTags("NotificationHub")
            .AllowAnonymous();

        return group;
    }
}
