using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.UserDtos;

public class UserUpdateEmailDTO
{
    [Required]
    public required string Email { get; set; }
}