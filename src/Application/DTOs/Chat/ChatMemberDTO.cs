using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Chat;

public class ChatMemberDTO
{
    [Required]
    public required int UserId { get; set; }

    [Required]
    public required string UserName { get; set; }

    [Required]
    public required string PublicKey { get; set; }

    [Required]
    public required string ProfilePicture { get; set; }
}
