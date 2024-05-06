using System.ComponentModel.DataAnnotations;
using Application.DTOs.UserDtos;

namespace Application.DTOs.ProjectDTOs;

public class ProjectInformationDTO
{
    [Required]
    public int Id { get; set; }

    [Required]
    public required string Name { get; set; }

    [Required]
    public UserInformationDTO? ProjectManager { get; set; }

}
