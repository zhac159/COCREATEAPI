using Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Application.Features.MediaFeature.GetUploadUris;

public static class GetUploadUrisEndpoint
{
    public static RouteGroupBuilder MapGetUploadUrisEndpoint(this RouteGroupBuilder group)
    {
        group
            .MapPost(
                "/get-upload-uris",
                async (IMediator mediator, GetUploadUrisRequest getuploadurisRequest) =>
                {
                    var getuploadurisResponse = await mediator.Send(getuploadurisRequest);
                    return Results.Ok(getuploadurisResponse);
                }
            )
            .Produces<List<GetUploadUrisResponse>>()
            .WithTags("Media");

        return group;
    }
}
