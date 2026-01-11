using System.ComponentModel.DataAnnotations;
using Application.DTOs.Chat;
using Application.DTOs.ProjectDTOs;

namespace Application.DTOs.UserDtos;

public class UserLoginResponseDTO
{
    [Required]
    public int UserId { get; set; }

    [Required]
    public required string Username { get; set; }

    [Required]
    public required string? PublicKey { get; set; }

    [Required]
    public required string Email { get; set; }

    [Required]
    public string? ProfilePicture { get; set; }

    [Required]
    public required int Coins { get; set; } = 0;

    [Required]
    public required List<ChatDTO> Chats { get; set; } = [];

    [Required]
    public required List<ProjectInfoDTO> ProjectsManaging { get; set; } = [];
}
