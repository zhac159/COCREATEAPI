using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Application.DTOs.MessageDTOs;

public class MessageDTO
{
    [Required]
    public required Guid Id { get; set; }

    [Required]
    public required int SenderId { get; set; }

    [Required]
    public required int TargetId { get; set; }

    [Required]
    public required ChatType ChatType { get; set; }


    public string? Content { get; set; }

    public string? Uri { get; set; }

    public MediaType? MediaType { get; set; }

    [Required]
    public required DateTime Date { get; set; }

    public Guid? ReplyMessageId { get; set; }

    [Required]
    public required string ChatId { get; set; }
}
