using Application.Features.ProjectFeature.CreateProject;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Application.Features.ProjectFeature;

public static class ProjectEndpointGroup
{
    public static RouteGroupBuilder MapProjectEndpoints(this RouteGroupBuilder group)
    {
        var projectGroup = group.MapGroup("/project").WithTags("Project");

        projectGroup.MapCreateProjectEndpoint();

        return group;
    }
}
