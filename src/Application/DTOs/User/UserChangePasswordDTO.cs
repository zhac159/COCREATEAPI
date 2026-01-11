using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.UserDtos;

public class UserChangePasswordDTO
{
    [Required]
    public required string OldPassword { get; set; }

    [Required]
    public required string NewPassword { get; set; }
}
