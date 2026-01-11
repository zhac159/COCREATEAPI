using Application.DTOs.MediaDTOs;
using Domain.Entities;
using Domain.Enums;

namespace Application.DTOs.ExperienceDTOs;

public class ExperienceCreateDTO
{
    public string? Description { get; set; }
    public List<MediaCreateDTO> Medias { get; set; } = new();
    public ExperienceType ExperienceType { get; set; }
    public int ExperienceId { get; set; }

    public Experience ToEntity(int userId)
    {
        return new Experience
        {
            Description = Description,
            UserId = userId,
            Medias = [.. Medias.Select((m, order) => m.ToExperienceMediaEntity(order))],
            ExperienceType = ExperienceType,
            ProjectId = ExperienceType.Equals(ExperienceType.Project) ? ExperienceId : null,
            ProjectRoleId = ExperienceType.Equals(ExperienceType.ProjectRole) ? ExperienceId : null
        };
    }
}