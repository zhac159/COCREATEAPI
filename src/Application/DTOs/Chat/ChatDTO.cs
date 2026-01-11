using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Application.DTOs.Chat;

public class ChatDTO
{
    [Required]
    public required int Id { get; set; }

    [Required]
    public required ChatType ChatType { get; set; }

    [Required]
    public required int ChatTypeId { get; set; }

    [Required]
    public string? GroupChatName { get; set; }

    [Required]
    public required List<ChatMemberDTO> ChatMembers { get; set; }
}
