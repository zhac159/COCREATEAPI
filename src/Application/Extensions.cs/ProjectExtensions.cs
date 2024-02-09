using Application.DTOs.ProjectDTO;
using Domain.Entities;

namespace Application.Extensions;

public static class ProjectExtensions
{
    public static ProjectDTO ToDTO(this Project project)
    {
        return new ProjectDTO
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            Uris = project.Uris,
            ProjectManager =  project.ProjectManager != null ? project.ProjectManager.ToInformationDTO(): null,
            ProjectRoles = project.ProjectRoles.Select(pr => pr.ToDTO()).ToList()
        };
    }

}