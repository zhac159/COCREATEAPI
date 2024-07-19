using Domain.Entities;
using Domain.Enums;

namespace Domain.Interfaces;

public interface IMessageStorageService
{
    Task AddMemberToGroupChatAsync(int chatId, ChatType chatType, int userId);
    Task AddMessageAsync(Message message, int userId);
    Task AddMessageReactionAsync(MessageReaction messageReaction, int userId);
    Task AddEncryptedKeyExchangeAsync(EncryptedKeyExchange encryptedKeyExchange);
    Task<IEnumerable<Message>> GetMessagesAsync(int userId);
    Task<IEnumerable<int>?> GetChatMemebersAsync(int chatId, ChatType chatType);
    Task<IEnumerable<EncryptedKeyExchange>> GetEncryptedKeyExchangesAsync(int userId);
    Task<IEnumerable<MessageReaction>> GetMessagesReactionsAsync(int userId);
    Task DeleteEncryptedKeyExchangeAsync(IEnumerable<Guid> encryptedKeyExchangeIds, int userId);
    Task DeleteMessageAsync(IEnumerable<Guid> messageIds, int userId);
    Task DeleteMessageReactionAsync(int userId);
    Task StoreOneTimeEmailTokenAsync(string token, int userId);
    Task<string?> GetOneTimeEmailTokenAsync(string token, int userId);
}
