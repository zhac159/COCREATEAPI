using Application.DTOs.UserDtos;
using Domain.Enums;

namespace Application.DTOs.ProjectRoleDTOs;

public class ProjectRoleDTO
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required List<Uri> Uris { get; set; }
    public required int Cost { get; set; }
    public required int Effort { get; set; }
    public required SkillType SkillType { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public required bool Remote { get; set; }
    public UserInformationDTO? Assignee { get; set; }
}
