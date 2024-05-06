using Application.DTOs.MediaDTOs;
using Application.DTOs.ReviewDTOs;
using Domain.Entities;
using Domain.Enums;

namespace Application.DTOs.ProjectDTOs;

public class ProjectCompleteDTO
{
    public int Id { get; set; }
    public List<MediaCreateDTO> Medias { get; set; } = new();
    public string? Description { get; set; }
    public List<ReviewCreateDTO> Reviews { get; set; } = new();

    public Experience ToExperienceEntity(int userId)
    {
        return new Experience
        {
            ProjectId = Id,
            ExperienceType = ExperienceType.Project,
            UserId = userId,
            Description = Description,
            Medias = Medias.Select((media, order) => media.ToExperienceMediaEntity(order)).ToList()
        };
    }
}
