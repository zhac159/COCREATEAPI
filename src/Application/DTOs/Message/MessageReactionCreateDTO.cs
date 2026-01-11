using Domain.Entities;
using Domain.Enums;

namespace Application.DTOs.MessageDTOs;

public class MessageReactionCreateDTO
{
    public Guid MessageId { get; set; }
    public int Multiplier { get; set; }
    public Emoji Reaction { get; set; }
    public required int TargetId { get; set; }
    public ChatType ChatType { get; set; }
    public required string ChatId { get; set; } 

    public MessageReaction ToEntity(int userId)
    {
        return new MessageReaction
        {
            MessageId = MessageId,
            Multiplier = Multiplier,
            Reaction = Reaction,
            UserId = userId,
        };
    }
}
