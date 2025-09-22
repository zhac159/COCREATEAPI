using Application.Features.Authentication;
using Microsoft.AspNetCore.Routing;

namespace Application.Configuration;

public static class FeatureGroupExtensions
{
    public static RouteGroupBuilder MapFeatureEndpoints(this RouteGroupBuilder group)
    {
        group.MapAuthenticationEndpoints();

        return group;
    }
}
