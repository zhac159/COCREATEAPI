using System.ComponentModel.DataAnnotations;
using Application.DTOs.SkillDTOs;

namespace Application.DTOs.UserDtos;

public class UserUpdateDTO
{
    public required string Username { get; set; }

    [EmailAddress]
    public required string Email { get; set; }
    public required string AboutYou { get; set; }
    public List<SkillUpdateDTO>? Skills { get; set; }
}
