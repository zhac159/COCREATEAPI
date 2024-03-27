using Application.DTOs.MediaDTOs;
using Domain.Enums;

namespace Application.DTOs.ProjectRoleDTOs;

public class ProjectRoleUpdateDTO
{
    public int Id { get; set; }
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
    public List<MediaUpdateDTO> Medias { get; set; } = new List<MediaUpdateDTO>();
}
