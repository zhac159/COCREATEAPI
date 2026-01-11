using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Application.DTOs.MessageDTOs;

public class MessageReactionDTO
{
    [Required]
    public Guid MessageId { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public int Multiplier { get; set; }

    [Required]
    public Emoji Reaction { get; set; }
}
