using Application.DTOs.MediaDTOs;
using Domain.Entities;
using Domain.Enums;
using NetTopologySuite.Geometries;

namespace Application.DTOs.ProjectRoleDTOs;

public class ProjectRoleCreateDTO
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required int Cost { get; set; }
    public required int Effort { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }
    public required SkillType SkillType { get; set; }
    public required double Longitude { get; set; }
    public required double Latitude { get; set; }
    public required string Address { get; set; }
    public List<string> Keywords { get; set; } = new List<string>();
    public required bool Remote { get; set; }
    public int ProjectId { get; set; }
    public required List<MediaCreateDTO> Medias { get; set; }

    public ProjectRole ToEntity()
    {
        return new ProjectRole
        {
            Name = Name,
            Description = Description,
            Cost = Cost,
            Effort = Effort,
            StartDate = StartDate,
            EndDate = EndDate,
            SkillType = SkillType,
            Location = new Point(Longitude, Latitude) { SRID = 4326 },
            Address = Address,
            Keywords = Keywords,
            Remote = Remote,
            ProjectId = ProjectId,
            Medias = Medias.Select((m, order) => m.ToProjectRoleMediaEntity(order)).ToList()
        };
    }
}
