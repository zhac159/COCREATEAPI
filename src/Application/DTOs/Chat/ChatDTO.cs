using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Application.DTOs.Chat;

public class ChatDTO
{
    [Required]
    public required ChatType ChatType { get; set; }

    [Required]
    public required int ChatIdType { get; set; }

    [Required]
    public required ChatMemberDTO[] ChatMembers { get; set; }
}
