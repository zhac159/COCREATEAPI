using Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Application.Features.UserFeature.GetProfileDetails;

public static class GetProfileDetailsEndpoint
{
    public static RouteGroupBuilder MapGetProfileDetailsEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet(
            "/profile-details",
            async (IMediator mediator) =>
            {
                var getprofiledetailsResponse = await mediator.Send(new GetProfileDetailsRequest());
                return Results.Ok(getprofiledetailsResponse);
            }
        );

        return group;
    }
}
