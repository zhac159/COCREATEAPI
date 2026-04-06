using Infrastructure.Entities;
using Infrastructure.Enums;

namespace Application.Features.Common.Records;

public abstract record ProjectRoleRecordBase
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required int Cost { get; set; }
    public required SkillType SkillType { get; set; }
    public required bool Remote { get; set; }
    public bool Completed { get; set; }
}

public record ProjectRoleRecord : ProjectRoleRecordBase
{
    public required int Id { get; set; }

    public static ProjectRoleRecord FromProjectRole(ProjectRole projectRole) =>
        new()
        {
            Id = projectRole.Id,
            Name = projectRole.Name,
            Description = projectRole.Description,
            Cost = projectRole.Cost,
            SkillType = projectRole.SkillType,
            Remote = projectRole.Remote,
            Completed = projectRole.Completed,
        };
}

public record CreateProjectRoleRecord : ProjectRoleRecordBase { }

public record UpdateProjectRoleRecord : ProjectRoleRecordBase
{
    public required int Id { get; set; }
}
