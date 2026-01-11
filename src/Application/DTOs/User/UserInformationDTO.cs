using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.UserDtos;

public class UserInformationDTO
{
    [Required]
    public int UserId { get; set; }
    [Required]
    public required string Username { get; set; }
    public double? Rating { get; set; }
    public string? PublicKey { get; set; }
}
