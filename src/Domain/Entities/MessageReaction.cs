using Domain.Enums;

namespace Domain.Entities;

public class MessageReaction
{
    public Guid MessageId { get; set; }
    public int UserId { get; set; }
    public int Multiplier { get; set; }
    public Emoji Reaction { get; set; }
}
