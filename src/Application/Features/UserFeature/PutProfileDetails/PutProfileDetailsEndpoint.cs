using Application.Features.UserFeature.Common;
using Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Application.Features.UserFeature.PutProfileDetails;

public static class PutProfileDetailsEndpoint
{
    public static RouteGroupBuilder MapPutProfileDetailsEndpoint(this RouteGroupBuilder group)
    {
        group
            .MapPut(
                "/profile-details",
                async (UpdateProfileDetails updateProfileDetails, IMediator mediator) =>
                {
                    var updateProfileDetailsResponse = await mediator.Send(
                        new UpdateProfileDetailsRequest() { ProfileDetails = updateProfileDetails }
                    );
                    return Results.Ok(updateProfileDetailsResponse);
                }
            )
            .Produces<ProfileDetails>()
            .WithTags("User");

        return group;
    }
}
