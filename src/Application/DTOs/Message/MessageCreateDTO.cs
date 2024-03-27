using Domain.Entities;
using Domain.Enums;

    namespace Application.DTOs.MessageDTOs;

public class MessageCreateDTO
{
    public required Guid Id { get; set; }
    public required int ChatId { get; set; }
    public ChatType ChatType { get; set; }
    public string? Content { get; set; }
    public string? Uri { get; set; }
    public MediaType? MediaType { get; set; }
    public required DateTime Date { get; set; }

    public Message ToEntity(int userId)
    {
        return new Message
        {
            Id = Guid.NewGuid(),
            SenderId = userId,
            ChatId = ChatId,
            ChatType = ChatType,
            Content = Content,
            Uri = Uri,
            MediaType = MediaType,
            Date = Date
        };
    }
}
