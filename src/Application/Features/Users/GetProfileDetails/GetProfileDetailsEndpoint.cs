using Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Application.Features.Users.GetProfileDetails;

public static class GetProfileDetailsEndpoint
{
    public static RouteGroupBuilder MapGetProfileDetailsEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost(
            "/getprofiledetails",
            async (IMediator mediator, GetProfileDetailsRequest getprofiledetailsRequest) =>
            {
                var getprofiledetailsResponse = await mediator.Send(getprofiledetailsRequest);
                return Results.Ok(getprofiledetailsResponse);
            }
        );

        return group;
    }
}
