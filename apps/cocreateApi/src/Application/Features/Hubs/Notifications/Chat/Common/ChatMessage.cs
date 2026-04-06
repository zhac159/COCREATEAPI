namespace Application.Features.Hubs.Notifications.Chat.Common;

public class ChatMessage
{
    public required Guid Id { get; set; }
    public required string Salt { get; set; }
    public required int ChatId { get; set; }
    public required int SenderId { get; set; }
    public string? Content { get; set; }
    public string? Uri { get; set; }
    public DateTime Date { get; set; }
    public Guid? ReplyMessageId { get; set; }
}
