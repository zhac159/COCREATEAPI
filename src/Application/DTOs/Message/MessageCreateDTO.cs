using System.ComponentModel.DataAnnotations;
using Domain.Entities;
using Domain.Enums;

namespace Application.DTOs.MessageDTOs;

public class MessageCreateDTO
{
    [Required]
    public required Guid Id { get; set; }

    [Required]
    public required string Salt { get; set; }

    [Required]
    public required int ChatId { get; set; }

    [Required]
    public required List<int> TargetUserIds { get; set; } = [];

    public string? Content { get; set; }

    public string? Uri { get; set; }

    [Required]
    public required DateTime Date { get; set; }

    public Guid? ReplyMessageId { get; set; }

    public List<Message> ToEntity(int userId)
    {
        return [.. TargetUserIds
            .Select(targetUserId => new Message
            {
                Id = Id,
                Salt = Salt,
                ChatId = ChatId,
                SenderId = userId,
                TargetUserId = targetUserId,
                Content = Content,
                Uri = Uri,
                Date = Date,
                ReplyMessageId = ReplyMessageId,
            })];
    }
}
