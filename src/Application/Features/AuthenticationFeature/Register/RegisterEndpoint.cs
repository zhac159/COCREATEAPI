using Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Application.Features.AuthenticationFeature.Register;

public static class RegisterEndpoint
{
    public static RouteGroupBuilder MapRegisterEndpoint(this RouteGroupBuilder group)
    {
        group
            .MapPost(
                "/register",
                async (IMediator mediator, RegisterRequest registerRequest) =>
                {
                    var registerResponse = await mediator.Send(registerRequest);
                    return Results.Ok(registerResponse);
                }
            )
            .WithTags("Authentication")
            .AllowAnonymous();

        return group;
    }
}
