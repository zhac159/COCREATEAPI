using Domain.Entities;
using Domain.Enums;

namespace Application.DTOs.MessageDTOs;

public class MessageCreateDTO
{
    public required Guid Id { get; set; }
    public required int TargetId { get; set; }
    public ChatType ChatType { get; set; }
    public string? Content { get; set; }
    public string? Uri { get; set; }
    public MediaType? MediaType { get; set; }
    public required DateTime Date { get; set; }
    public Guid? ReplyMessageId { get; set; }

    public Message ToEntity(int userId)
    {
        return new Message
        {
            Id = Id,
            SenderId = userId,
            TargetId = TargetId,
            ChatType = ChatType,
            Content = Content,
            ReplyMessageId = ReplyMessageId,
            Uri = Uri,
            MediaType = MediaType,
            Date = Date
        };
    }
}
