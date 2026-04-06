using Application.Features.AuthenticationFeature.Common;
using Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Application.Features.AuthenticationFeature.Login;

public static class LoginEndpoint
{
    public static RouteGroupBuilder MapLoginEndpoint(this RouteGroupBuilder group)
    {
        group
            .MapPost(
                "/login",
                async (IMediator mediator, LoginRequest loginRequest) =>
                {
                    var loginResponse = await mediator.Send(loginRequest);
                    return Results.Ok(loginResponse);
                }
            )
            .WithTags("Authentication")
            .Produces<LoginResponse>()
            .AllowAnonymous();

        return group;
    }
}
