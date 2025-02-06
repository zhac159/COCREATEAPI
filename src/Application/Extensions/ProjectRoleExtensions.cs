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
            SkillType = projectRole.SkillType,
            Longitude = projectRole.Location.X,
            Latitude = projectRole.Location.Y,
            Address = projectRole.Address,
            Keywords = projectRole.Keywords,
            ProjectId = projectRole.ProjectId,
            Completed = projectRole.Completed,
            Remote = projectRole.Remote,
            Enquiries = projectRole.Enquiries.Select(enquiry => enquiry.ToDTO()).ToList(),
            Assignee = projectRole.Assignee?.ToInformationDTO(),
            Medias = projectRole
                .Medias.OrderBy(media => media.Order)
                .Select(media => media.ToDTO())
                .ToList()
        };
    }

    public static void UpdateFromDTO(
        this ProjectRole projectRole,
        ProjectRoleUpdateDTO projectRoleUpdateDTO
    )
    {
        if (projectRoleUpdateDTO is null)
            return;
        projectRole.Name = projectRoleUpdateDTO.Name;
        projectRole.Description = projectRoleUpdateDTO.Description;
        projectRole.Cost = projectRoleUpdateDTO.Cost;
        projectRole.Effort = 0; // or set to 0 if that is required
        projectRole.SkillType = projectRoleUpdateDTO.SkillType;
        projectRole.Remote = projectRoleUpdateDTO.Remote;
        // Adjust the point coordinates based on your DTO (assuming Longitude and Latitude exist)
        projectRole.Location = new Point(0, 0) { SRID = 4326 };
        projectRole.Address = "Address";
        projectRole.Medias = [];
        projectRole.Keywords = [];

        // projectRole.Name = projectRoleUpdateDTO.Name;
        // projectRole.Description = projectRoleUpdateDTO.Description;
        // projectRole.Cost = projectRoleUpdateDTO.Cost;
        // projectRole.SkillType = projectRoleUpdateDTO.SkillType;
        // projectRole.Location = new Point(0, 0) { SRID = 4326 };
        // projectRole.Address = "";
        // projectRole.Keywords = [];
        // projectRole.Remote = projectRoleUpdateDTO.Remote;

        // if (projectRoleUpdateDTO.Medias is not null)
        // {
        //     var updateMediaIds = projectRoleUpdateDTO.Medias.Select(media => media.Id).ToList();

        //     var mediasToDelete = projectRole
        //         .Medias.Where(media => !updateMediaIds.Contains(media.Id))
        //         .ToList();

        //     foreach (var media in mediasToDelete)
        //     {
        //         var fileName = Path.GetFileName(new Uri(media.Uri).LocalPath);

        //         await storageService.DeleteFile(fileName, "projectroles");
        //     }

        //     foreach (var updatedMedia in projectRoleUpdateDTO.Medias)
        //     {
        //         var originalMedia = projectRole.Medias.FirstOrDefault(media =>
        //             media.Id == updatedMedia.Id
        //         );

        //         if (originalMedia != null && originalMedia.Uri != updatedMedia.Uri)
        //         {
        //             var fileName = Path.GetFileName(new Uri(originalMedia.Uri).LocalPath);
        //             await storageService.DeleteFile(fileName, "projectroles");
        //         }
        //     }

        //     projectRole.Medias?.RemoveAll(m =>
        //         !projectRoleUpdateDTO.Medias.Any(mu => mu.Id == m.Id)
        //     );

        //     projectRole.Medias = projectRoleUpdateDTO
        //         .Medias.Select(
        //             (mediaUpdateDTO, order) =>
        //             {
        //                 var media = projectRole.Medias?.FirstOrDefault(m =>
        //                     m.Id == mediaUpdateDTO.Id
        //                 );
        //                 if (media is not null)
        //                 {
        //                     media.UpdateFromDTO(mediaUpdateDTO, order);
        //                     return media;
        //                 }
        //                 return mediaUpdateDTO.ToProjectRoleMediaEntity(order);
        //             }
        //         )
        //         .ToList();
        // }
    }
}
