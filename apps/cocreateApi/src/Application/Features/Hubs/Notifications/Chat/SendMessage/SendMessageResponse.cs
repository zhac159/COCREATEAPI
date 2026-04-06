namespace Application.Features.Hubs.Notifications.Chat.SendMessage;

public class SendMessageResponse
{
    public required string MessageId { get; set; } = string.Empty;
    public required bool Success { get; set; }
}
