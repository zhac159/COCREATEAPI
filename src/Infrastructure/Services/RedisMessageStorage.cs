using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Infrastructure.Services;

public class RedisMessageStorage : IMessageStorageService
{
    private readonly IRedisService redisService;
    private readonly IDatabase database;

    public RedisMessageStorage(IRedisService redisService)
    {
        this.redisService = redisService;
        database = redisService.GetDatabase();
    }

    public async Task AddMemberToGroupChatAsync(int chatId, ChatType chatType, int userId)
    {
        var key = $"chat:{chatType}-{chatId}";
        await database.ListRightPushAsync(key, userId);
    }

    public async Task<IEnumerable<int>?> GetChatMemebersAsync(int chatId, ChatType chatType)
    {
        var key = $"chat:{chatType}-{chatId}";
        var values = await database.ListRangeAsync(key);
        return values.Select(value => (int)value).ToArray();
    }

    public async Task AddMessageAsync(Message message, int userId)
    {
        var key = $"user:{userId}:messages";
        var messageString = JsonConvert.SerializeObject(message);
        await database.HashSetAsync(key, message.Id.ToString(), messageString);
    }

    public async Task AddMessageReactionAsync(MessageReaction messageReaction, int userId)
    {
        var key = $"user:{userId}:messageReactions";
        var messageReactionString = JsonConvert.SerializeObject(messageReaction);
        var guid = Guid.NewGuid().ToString();
        await database.HashSetAsync(key, guid, messageReactionString);
    }

    public async Task AddEncryptedKeyExchangeAsync(EncryptedKeyExchange encryptedKeyExchange)
    {
        var key = $"user:{encryptedKeyExchange.TargetId}:encryptedKeyExchanges";
        var encryptedKeyExchangeString = JsonConvert.SerializeObject(encryptedKeyExchange);
        await database.HashSetAsync(
            key,
            encryptedKeyExchange.Id.ToString(),
            encryptedKeyExchangeString
        );
    }

    public async Task<IEnumerable<EncryptedKeyExchange>> GetEncryptedKeyExchangesAsync(int userId)
    {
        var key = $"user:{userId}:encryptedKeyExchanges";
        var encryptedKeyExchanges = await database.HashGetAllAsync(key);
        return encryptedKeyExchanges
            .Select(encryptedKeyExchange =>
                JsonConvert.DeserializeObject<EncryptedKeyExchange>(encryptedKeyExchange.Value)
            )
            .ToArray();
    }

    public async Task<IEnumerable<Message>> GetMessagesAsync(int userId)
    {
        var key = $"user:{userId}:messages";
        var messages = await database.HashGetAllAsync(key);
        return messages
            .Select(message => JsonConvert.DeserializeObject<Message>(message.Value))
            .ToArray();
    }

    public async Task<IEnumerable<MessageReaction>> GetMessagesReactionsAsync(int userId)
    {
        var key = $"user:{userId}:messageReactions";
        var messageReactions = await database.HashGetAllAsync(key);
        return messageReactions
            .Select(messageReaction =>
                JsonConvert.DeserializeObject<MessageReaction>(messageReaction.Value)
            )
            .ToArray();
    }

    public async Task DeleteEncryptedKeyExchangeAsync(
        IEnumerable<Guid> encryptedKeyExchangeIds,
        int userId
    )
    {
        var key = $"user:{userId}:encryptedKeyExchanges";
        var encryptedKeyExchangeIdsArray = encryptedKeyExchangeIds
            .Select(encryptedKeyExchangeId => (RedisValue)encryptedKeyExchangeId.ToString())
            .ToArray();
        await database.HashDeleteAsync(key, encryptedKeyExchangeIdsArray);
    }

    public async Task DeleteMessageAsync(IEnumerable<Guid> messageIds, int userId)
    {
        var key = $"user:{userId}:messages";
        var messageIdsArray = messageIds
            .Select(messageId => (RedisValue)messageId.ToString())
            .ToArray();
        await database.HashDeleteAsync(key, messageIdsArray);
    }

    public async Task DeleteMessageReactionAsync(int userId)
    {
        var key = $"user:{userId}:messageReactions";
        await database.KeyDeleteAsync(key);
    }
}
