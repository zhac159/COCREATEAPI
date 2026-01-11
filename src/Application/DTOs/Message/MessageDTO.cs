using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Application.DTOs.MessageDTOs;

public class MessageDTO
{
    [Required]
    public required Guid Id { get; set; }

    [Required]
    public required string Salt { get; set; }

    [Required]
    public required int ChatId { get; set; }

    [Required]
    public required int SenderId { get; set; }
    public string? Content { get; set; }
    public string? Uri { get; set; }

    [Required]
    public DateTime Date { get; set; }
    public Guid? ReplyMessageId { get; set; }
}
