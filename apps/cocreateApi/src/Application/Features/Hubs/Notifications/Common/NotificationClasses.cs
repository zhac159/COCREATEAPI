using Application.Features.Hubs.Notifications.Chat.SendMessage;

namespace Application.Features.Hubs.Notifications.Common;

// This class is just a container for the notification-related classes used top autogenerate the classes via Orval.ts

public class NotificationClasses
{
    public required SendMessageRequest SendMessageRequest { get; set; }
    public required SendMessageResponse SendMessageResponse { get; set; }
}
