using Application.Features.AuthenticationFeature;
using Application.Features.Hubs.Notifications.Common;
using Application.Features.MediaFeature;
using Application.Features.UserFeature;
using Microsoft.AspNetCore.Routing;

namespace Application.Configuration;

public static class FeatureGroupExtensions
{
    public static RouteGroupBuilder MapFeatureEndpoints(this RouteGroupBuilder group)
    {
        group.MapAuthenticationEndpoints().MapNotificationDummyEndpoint();
        group.MapUserEndpoints();
        group.MapMediaEndpoints();

        return group;
    }
}
