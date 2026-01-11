using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.UserDtos;

public class UserVerifyEmailDTO
{
    [Required]
    public required string Token { get; set; }
}