using Application.Features.Authentication;
using Application.Features.Hubs.Notifications.Common;
using Application.Features.UserFeature;
using Microsoft.AspNetCore.Routing;

namespace Application.Configuration;

public static class FeatureGroupExtensions
{
    public static RouteGroupBuilder MapFeatureEndpoints(this RouteGroupBuilder group)
    {
        group.MapAuthenticationEndpoints();
        group.MapNotificationDummyEndpoint();
        group.MapUserEndpoints();

        return group;
    }
}
