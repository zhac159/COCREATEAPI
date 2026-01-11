using System.ComponentModel.DataAnnotations;
using Application.DTOs.UserDtos;

namespace Application.DTOs.ProjectDTOs;

public class ProjectInfoDTO
{
    [Required]
    public int Id { get; set; }

    [Required]
    public required string Name { get; set; }
}
