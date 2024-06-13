using Application.DTOs.ProjectDTOs;
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
            Completed = project.Completed,
            Description = project.Description,
            ProjectManager = project.ProjectManager?.ToInformationDTO(),
            ProjectRoles = project.ProjectRoles.Select(pr => pr.ToDTO()).ToList(),
            Medias = project
                .Medias.OrderBy(media => media.Order)
                .Select(media => media.ToDTO())
                .ToList(),
            AssetOffers = project.AssetOffers.Select(assetOffer => assetOffer.ToDTO()).ToList(),
        };
    }

    public static ProjectInformationDTO ToInformationDTO(this Project project)
    {
        return new ProjectInformationDTO
        {
            Id = project.Id,
            Name = project.Name,
            ProjectManager = project.ProjectManager?.ToInformationDTO(),
        };
    }

    public static ProjectCompletedDTO ToCompletedDTO(this Project project)
    {
        return new ProjectCompletedDTO
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            ProjectManager = project.ProjectManager?.ToInformationDTO(),
            ProjectRoles = project.ProjectRoles.Select(pr => pr.ToDTO()).ToList(),
            Medias = project.Medias.Select(media => media.ToDTO()).ToList(),
            ExperiencesMedias = project
                .Experiences.Concat(project.ProjectRoles.SelectMany(role => role.Experiences))
                .SelectMany(e => e.Medias)
                .Select(media => media.ToDTO())
                .ToList(),
        };
    }
}
