using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Application.Features.ProjectFeature;

public static class ProjectEndpointGroup
{
    public static RouteGroupBuilder MapProjectEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/project");

        return group;
    }
}
