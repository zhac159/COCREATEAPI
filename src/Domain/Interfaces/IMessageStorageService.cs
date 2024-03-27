using Domain.Entities;
using Domain.Enums;

namespace Domain.Interfaces;

public interface IMessageStorageService
{
    Task AddChatAsync(int chatId, ChatType chatType, IEnumerable<int> userIds);
    Task<IEnumerable<int>?> GetChatMemebersAsync(int chatId, ChatType chatType);
    Task AddMessageAsync(Message message, int userId);
    Task DeleteMessageAsync(IEnumerable<Guid> messageIds, int userId);
    Task <IEnumerable<Message>> GetMessagesAsync(int userId);
}