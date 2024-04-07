using Domain.Entities;
using Domain.Enums;

namespace Domain.Interfaces;

public interface IMessageStorageService
{
    Task AddMemberToGroupChatAsync(int chatId, ChatType chatType, int userId);
    Task<IEnumerable<int>?> GetChatMemebersAsync(int chatId, ChatType chatType);
    Task AddMessageAsync(Message message, int userId);
    Task AddEncryptedKeyExchangeAsync(EncryptedKeyExchange encryptedKeyExchange);
    Task<IEnumerable<EncryptedKeyExchange>> GetEncryptedKeyExchangesAsync(int userId);
    Task DeleteEncryptedKeyExchangeAsync(IEnumerable<Guid> encryptedKeyExchangeIds, int userId);
    Task DeleteMessageAsync(IEnumerable<Guid> messageIds, int userId);
    Task <IEnumerable<Message>> GetMessagesAsync(int userId);
}