using System.ComponentModel.DataAnnotations;
using Application.DTOs.MediaDTOs;
using Domain.Entities;
using Domain.Enums;
using NetTopologySuite.Geometries;

namespace Application.DTOs.ProjectRoleDTOs;

public class ProjectRoleUpdateDTO
{
    public int Id { get; set; }

    [Required]
    public required string Name { get; set; }

    [Required]
    public required string Description { get; set; }

    [Required]
    public required int Cost { get; set; }

    [Required]
    public required SkillType SkillType { get; set; }

    [Required]
    public required bool Remote { get; set; }


    public ProjectRole ToEntity()
    {
        return new ProjectRole
        {
            Id = Id,
            Name = Name,
            Description = Description,
            Cost = Cost,
            Effort = 0,
            SkillType = SkillType,
            Remote = Remote,
            Location = new Point(0, 0) { SRID = 4326 },
            Address = "Address",
            Medias = [],
            Keywords = [],
        };
    }
}
