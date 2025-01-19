using Domain.Enums;

namespace Domain.Entities;

public class Chat
{
    public int Id { get; set; }
    public ChatType ChatType { get; set; }
    public int ChatTypeId { get; set; } 
    public string? GroupChatName { get; set; }
    public required List<ChatMembership> ChatMemberships { get; set; } = [];
}
