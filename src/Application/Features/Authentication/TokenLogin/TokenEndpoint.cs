using Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Application.Features.Authentication.TokenLogin;

public static class TokenEndpoint
{
    public static RouteGroupBuilder MapTokenEndpoint(this RouteGroupBuilder group)
    {
        group
            .MapPost(
                "/token",
                async (IMediator mediator, TokenRequest tokenRequest) =>
                {
                    var tokenResponse = await mediator.Send(tokenRequest);
                    return Results.Ok(tokenResponse);
                }
            )
            .WithTags("Authentication")
            .AllowAnonymous();

        return group;
    }
}
