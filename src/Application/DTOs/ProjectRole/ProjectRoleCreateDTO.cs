using Domain.Entities;
using Domain.Enums;

namespace Application.DTOs.ProjectRoleDTOs;

public class ProjectRoleCreateDTO
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required List<string> FileSrcs { get; set; } = new List<string>();
    public required int Cost { get; set; }
    public required int Effort { get; set; }
    public required SkillType SkillType { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public required bool Remote { get; set; }

    public ProjectRole ToEntity(int projectId)
    {
        return new ProjectRole
        {
            Name = Name,
            Description = Description,
            FileSrcs = FileSrcs,
            Cost = Cost,
            Effort = Effort,
            SkillType = SkillType,
            Longitude = Longitude,
            Latitude = Latitude,
            Remote = Remote,
            ProjectId = projectId
        };
    }
}
