using System.ComponentModel.DataAnnotations;
using Domain.Entities;
using Domain.Enums;

namespace Application.DTOs.MessageDTOs;

public class MessageCreateDTO
{
    [Required]
    public required Guid Id { get; set; }

    [Required]
    public required int TargetId { get; set; }

    [Required]
    public ChatType ChatType { get; set; }

    public string? Content { get; set; }

    public string? Uri { get; set; }

    public MediaType? MediaType { get; set; }

    [Required]
    public required DateTime Date { get; set; }

    public Guid? ReplyMessageId { get; set; }

    [Required]
    public required string ChatId { get; set; }

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
            Date = Date,
            ChatId = ChatId
        };
    }
}
