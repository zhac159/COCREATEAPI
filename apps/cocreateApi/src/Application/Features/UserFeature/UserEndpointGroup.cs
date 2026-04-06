using Application.Features.UserFeature.GetProfileDetails;
using Application.Features.UserFeature.PutProfileDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Application.Features.UserFeature;

public static class UserEndpointGroup
{
    public static RouteGroupBuilder MapUserEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/user");

        group.MapGetProfileDetailsEndpoint();
        group.MapPutProfileDetailsEndpoint();

        return group;
    }
}
