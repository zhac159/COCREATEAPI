using Application.Features.Authentication.Login;
using Application.Features.Authentication.Register;
using Application.Features.Authentication.TokenLogin;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Application.Features.Users;

public static class AuthenticationEndpointGroup
{
    public static RouteGroupBuilder MapUserEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/authentication");

        group.MapLoginEndpoint().MapRegisterEndpoint().MapTokenEndpoint();

        return group;
    }
}
