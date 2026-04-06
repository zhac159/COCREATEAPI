using Application.Features.Common.Records;
using Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Application.Features.ProjectFeature.CreateProject;

public static class CreateProjectEndpoint
{
    public static RouteGroupBuilder MapCreateProjectEndpoint(this RouteGroupBuilder group)
    {
        group
            .MapPost(
                "/",
                async (IMediator mediator, CreateProjectRequest createProjectRequest) =>
                {
                    var project = await mediator.Send(createProjectRequest);
                    return Results.Ok(project);
                }
            )
            .Produces<ProjectRecord>();

        return group;
    }
}
