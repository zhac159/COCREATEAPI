using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.UserDtos;

public class UserTokenLoginDTO
{
    [Required]
    public required string Token { get; set; }
}
