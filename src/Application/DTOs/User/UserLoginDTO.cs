using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.UserDtos;

public class UserLoginDTO
{
    [Required]
    public required string UsernameOrEmail { get; set; }
    [Required]
    public required string Password { get; set; }
}
