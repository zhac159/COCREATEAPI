using Application.Features.AuthenticationFeature.Login;
using Application.Features.AuthenticationFeature.Register;
using Application.Features.AuthenticationFeature.TokenLogin;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Application.Features.AuthenticationFeature;

public static class AuthenticationEndpointGroup
{
    public static RouteGroupBuilder MapAuthenticationEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/authentication");

        group.MapLoginEndpoint().MapRegisterEndpoint().MapTokenEndpoint();

        return group;
    }
}
