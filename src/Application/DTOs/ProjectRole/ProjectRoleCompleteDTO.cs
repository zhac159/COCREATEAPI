using Application.DTOs.MediaDTOs;
using Application.DTOs.ReviewDTOs;
using Domain.Entities;
using Domain.Enums;

namespace Application.DTOs.ProjectRoleDTOs;

public class ProjectRoleCompleteDTO
{
    public int Id { get; set; }
    public List<MediaCreateDTO> Medias { get; set; } = new();
    public string? Description { get; set; }
    public List<ReviewCreateDTO> Reviews { get; set; } = new();

    public Experience ToExperienceEntity(int userId)
    {
        return new Experience
        {
            ProjectRoleId = Id,
            ExperienceType = ExperienceType.ProjectRole,
            UserId = userId,
            Description = Description,
            Medias = [.. Medias.Select((media, order) => media.ToExperienceMediaEntity(order))]
        };
    }
}
