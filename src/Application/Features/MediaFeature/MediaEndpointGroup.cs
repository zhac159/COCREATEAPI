using Application.Features.AuthenticationFeature.Login;
using Application.Features.MediaFeature.GetUploadUris;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Application.Features.MediaFeature;

public static class MediaEndpointGroup
{
    public static RouteGroupBuilder MapMediaEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/media");

        group.MapLoginEndpoint().MapGetUploadUrisEndpoint();

        return group;
    }
}
