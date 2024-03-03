using Application.DTOs.ProjectRoleDTOs;
using Application.Interfaces;
using Domain.Entities;
using NetTopologySuite.Geometries;

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
            Cost = projectRole.Cost,
            Effort = projectRole.Effort,
            StartDate = projectRole.StartDate,
            EndDate = projectRole.EndDate,
            SkillType = projectRole.SkillType,
            Longitude = projectRole.Location.X,
            Latitude = projectRole.Location.Y,
            Address = projectRole.Address,
            Keywords = projectRole.Keywords,
            Remote = projectRole.Remote,
            Assignee = projectRole.Assignee?.ToInformationDTO(),
            Medias = projectRole
                .Medias.OrderBy(media => media.Order)
                .Select(media => media.ToDTO())
                .ToList()
        };
    }

    public static async Task UpdateFromDTOAsync(
        this ProjectRole projectRole,
        ProjectRoleUpdateDTO projectRoleUpdateDTO,
        IStorageService storageService
    )
    {
        projectRole.Name = projectRoleUpdateDTO.Name;
        projectRole.Description = projectRoleUpdateDTO.Description;
        projectRole.Cost = projectRoleUpdateDTO.Cost;
        projectRole.Effort = projectRoleUpdateDTO.Effort;
        projectRole.StartDate = projectRoleUpdateDTO.StartDate;
        projectRole.EndDate = projectRoleUpdateDTO.EndDate;
        projectRole.SkillType = projectRoleUpdateDTO.SkillType;
        projectRole.Location = new Point(
            projectRoleUpdateDTO.Longitude,
            projectRoleUpdateDTO.Latitude
        )
        {
            SRID = 4326
        };
        projectRole.Address = projectRoleUpdateDTO.Address;
        projectRole.Keywords = projectRoleUpdateDTO.Keywords;
        projectRole.Remote = projectRoleUpdateDTO.Remote;

        if (projectRoleUpdateDTO.Medias is not null)
        {
            var updateMediaIds = projectRoleUpdateDTO.Medias.Select(media => media.Id).ToList();

            var mediasToDelete = projectRole
                .Medias.Where(media => !updateMediaIds.Contains(media.Id))
                .ToList();

            foreach (var media in mediasToDelete)
            {
                var fileName = Path.GetFileName(new Uri(media.Uri).LocalPath);

                await storageService.DeleteFile(fileName, "projectroles");
            }

            foreach (var updatedMedia in projectRoleUpdateDTO.Medias)
            {
                var originalMedia = projectRole.Medias.FirstOrDefault(
                    media => media.Id == updatedMedia.Id
                );

                if (originalMedia != null && originalMedia.Uri != updatedMedia.Uri)
                {
                    var fileName = Path.GetFileName(new Uri(originalMedia.Uri).LocalPath);
                    await storageService.DeleteFile(fileName, "projectroles");
                }
            }

            projectRole.Medias?.RemoveAll(
                m => !projectRoleUpdateDTO.Medias.Any(mu => mu.Id == m.Id)
            );
            
            
            projectRole.Medias = projectRoleUpdateDTO
                .Medias.Select(
                    (mediaUpdateDTO, order) =>
                    {
                        var media = projectRole.Medias?.FirstOrDefault(
                            m => m.Id == mediaUpdateDTO.Id
                        );
                        if (media is not null)
                        {
                            media.UpdateFromDTO(mediaUpdateDTO, order);
                            return media;
                        }
                        return mediaUpdateDTO.ToProjectRoleMediaEntity(order);
                    }
                )
                .ToList();

        }
    }
}
