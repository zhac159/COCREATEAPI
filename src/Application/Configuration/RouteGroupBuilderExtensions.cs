using Application.Features.Authentication;
using Application.Features.Hubs.Notifications.Common;
using Microsoft.AspNetCore.Routing;

namespace Application.Configuration;

public static class FeatureGroupExtensions
{
    public static RouteGroupBuilder MapFeatureEndpoints(this RouteGroupBuilder group)
    {
        group.MapAuthenticationEndpoints().MapNotificationDummyEndpoint();

        return group;
    }
}
