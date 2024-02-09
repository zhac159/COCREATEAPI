using Application.DTOs.PortofolioContentDTOs;
using Application.DTOs.ProjectRoleDTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Extensions;

public static class ProjectRoleeExtensions
{
    public static ProjectRoleDTO ToDTO(this ProjectRole projectRole)
    {
        return new ProjectRoleDTO
        {
            Id = projectRole.Id,
            Name = projectRole.Name,
            Description = projectRole.Description,
            Uris = projectRole.Uris,
            Cost = projectRole.Cost,
            Effort = projectRole.Effort,
            SkillType = projectRole.SkillType,
            Longitude = projectRole.Longitude,
            Latitude = projectRole.Latitude,
            Remote = projectRole.Remote,
            Assignee = projectRole.Assignee?.ToInformationDTO()
        };
    }
}
