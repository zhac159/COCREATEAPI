using System.ComponentModel.DataAnnotations;
using Application.DTOs.MediaDTOs;
using Domain.Entities;

namespace Application.DTOs.ProjectDTOs;

public class ProjectCreateDTO
{
    [Required]
    public required string Name { get; set; }
    [Required]
    public required string Description { get; set; }
    [Required]
    public required List<MediaCreateDTO> Medias { get; set; } = [];

    public Project ToEntity(int projectManagerId)
    {
        return new Project
        {
            Name = Name,
            Description = Description,
            ProjectManagerId = projectManagerId,
            Medias = Medias.Select((m, order) => m.ToProjectMediaEntity(order)).ToList()
        };
    }
}
