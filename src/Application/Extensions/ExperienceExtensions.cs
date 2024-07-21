using Application.DTOs.ExperienceDTOs;
using Domain.Entities;

namespace Application.Extensions;

public static class ExperienceExtensions
{
    public static ExperienceDTO ToDTO(this Experience experience)
    {
        return new ExperienceDTO
        {
            Id = experience.Id,
            Description = experience.Description,
            Medias = experience.Medias.Select(media => media.ToDTO()).ToList(),
            ExperienceType = experience.ExperienceType,
            ProjectId = experience.ProjectId,
            ProjectRoleId = experience.ProjectRoleId,
            ProjectRole = experience.ProjectRole?.ToDTO(),
            Project = experience.Project?.ToDTO()
        };
    } 
}