namespace Application.DTOs.Chat;

public class EntityWithChatDTO<T> where T : class
{
    public required T Entity { get; set; }
    public required ChatDTO Chat { get; set; }
}