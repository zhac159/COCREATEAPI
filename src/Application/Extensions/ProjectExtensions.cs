using Application.DTOs;
using Application.DTOs.ProjectDTOs;
using Domain.Entities;
using NetTopologySuite.Geometries;

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
            Date = project.Date,
            Location = new LocationDTO
            {
                Longitude = project.Location.X,
                Latitude = project.Location.Y,
                Address = project.Address,
            },
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
            ProjectManager = project.ProjectManager!.ToInformationDTO()!,
            ProjectRoles = project!.ProjectRoles.Select(pr => pr.ToDTO()).ToList(),
            Medias = project.Medias.Select(media => media.ToDTO()).ToList(),
            ExperiencesMedias = project
                .Experiences.Concat(project.ProjectRoles.SelectMany(role => role.Experiences))
                .SelectMany(e => e.Medias)
                .Select(media => media.ToDTO())
                .ToList(),
        };
    }

    public static ProjectInfoDTO ToInfoDTO(this Project project)
    {
        return new ProjectInfoDTO { Id = project.Id, Name = project.Name, };
    }

    public static void UpdateFromDTO(this Project project, ProjectUpdateDTO projectUpdateDTO)
    {
        project.Name = projectUpdateDTO.Name;
        project.Description = projectUpdateDTO.Description;
        project.Date = projectUpdateDTO.Date;
        project.Location = new Point(
            projectUpdateDTO.Location.Longitude,
            projectUpdateDTO.Location.Latitude
        )
        {
            SRID = 4326
        };
        project.Address = projectUpdateDTO.Location.Address;

        if (projectUpdateDTO.Medias is not null)
        {
            project.Medias?.Clear();
            project.Medias = projectUpdateDTO
                .Medias.Select((media, order) => media.ToProjectMediaEntity(order))
                .ToList();
        }

        project.ProjectRoles = projectUpdateDTO
            .ProjectRoles.Select(prudto =>
            {
                var projectRole = project.ProjectRoles.First(pr => pr.Id == prudto.Id);
                projectRole.UpdateFromDTO(prudto);
                return projectRole;
            })
            .ToList();
    }
}
