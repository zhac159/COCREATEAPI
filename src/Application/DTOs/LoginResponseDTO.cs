using System.ComponentModel.DataAnnotations;
using Application.DTOs.UserDtos;

namespace Application.DTOs;

public class LoginResponseDTO
{
    [Required]
    public required string Token { get; set; }

    [Required]
    public required UserLoginResponseDTO User { get; set; }
}
