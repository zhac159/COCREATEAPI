using Application.Features.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Application.Configuration;

public static class FeatureGroupExtensions
{
    public static RouteGroupBuilder MapFeatureEndpoints(this RouteGroupBuilder group)
    {
        group.MapAuthenticationEndpoints();

        group
            .MapGet(
                "/test",
                () =>
                {
                    return Results.Ok();
                }
            )
            .WithTags("test")
            .RequireAuthorization();

        return group;
    }
}
