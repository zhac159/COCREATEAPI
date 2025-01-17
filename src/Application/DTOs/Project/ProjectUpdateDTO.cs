using System.ComponentModel.DataAnnotations;
using Application.DTOs.MediaDTOs;
using Application.DTOs.ProjectRoleDTOs;
using Domain.Entities;
using NetTopologySuite.Geometries;

namespace Application.DTOs.ProjectDTOs;

public class ProjectUpdateDTO
{
    public int Id { get; set; }

    [Required]
    public required string Name { get; set; }

    [Required]
    public required string Description { get; set; }

    [Required]
    public required DateTime Date { get; set; }

    [Required]
    public required LocationDTO Location { get; set; }

    [Required]
    public required List<MediaUpdateDTO> Medias { get; set; } = [];

    [Required]
    public required List<ProjectRoleUpdateDTO> ProjectRoles { get; set; } = [];

    public Project ToEntity(int projectManagerId)
    {
        return new Project
        {
            Id = Id,
            ProjectManagerId = projectManagerId,
            Medias = Medias.Select((m, order) => m.ToProjectMediaEntity(order)).ToList(),
            Name = Name,
            Description = Description,
            Date = Date,
            Location = new Point(Location.Longitude, Location.Latitude) { SRID = 4326 },
            Address = Location.Address,
            ProjectRoles = ProjectRoles.Select(pr => pr.ToEntity()).ToList()
        };
    }
}
