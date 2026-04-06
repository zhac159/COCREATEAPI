using Infrastructure.Entities;

namespace Application.Features.Common.Records;

public abstract record ProjectRecordBase
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required DateTime Date { get; set; }
    public required LocationRecord Location { get; set; }
}

public record ProjectRecord : ProjectRecordBase
{
    public required int Id { get; set; }
    public List<MediaRecord> Medias { get; set; } = [];
    public List<ProjectRoleRecord> ProjectRoles { get; set; } = [];

    public static ProjectRecord FromProject(Project project) =>
        new()
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            Date = project.Date,
            Location = new LocationRecord
            {
                Latitude = project.Location.Y,
                Longitude = project.Location.X,
                Address = project.Address,
            },
            Medias =
            [
                .. project
                    .ProjectMedias.OrderBy(pm => pm.Order)
                    .Select(pm => new MediaRecord
                    {
                        Id = pm.Id,
                        Uri = pm.Uri,
                        MediaType = pm.MediaType,
                    }),
            ],
            ProjectRoles = [.. project.ProjectRoles.Select(ProjectRoleRecord.FromProjectRole)],
        };
}

public record CreateProjectRecord : ProjectRecordBase
{
    public List<CreateMediaRecord> Medias { get; set; } = [];
    public List<CreateProjectRoleRecord> ProjectRoles { get; set; } = [];
}

public record UpdateProjectRecord : ProjectRecordBase
{
    public required int Id { get; set; }
    public List<UpdateMediaRecord> Medias { get; set; } = [];
    public List<UpdateProjectRoleRecord> ProjectRoles { get; set; } = [];
}
