using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.UserDtos;

public class UserLoginDTO
{
    [Required]
    public required string Username { get; set; }
    [Required]
    public required string Password { get; set; }
}
