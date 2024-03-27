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

    public async Task AddChatAsync(int chatId, ChatType chatType, IEnumerable<int> userIds)
    {
        var key = $"chat:{chatType}-{chatId}";
        var values = userIds.Select(userId => (RedisValue)userId).ToArray();
        await database.ListRightPushAsync(key, values);
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

    public async Task DeleteMessageAsync(IEnumerable<Guid> messageIds, int userId)
    {
        var key = $"user:{userId}:messages";
        var messageIdsArray = messageIds
            .Select(messageId => (RedisValue)messageId.ToString())
            .ToArray();
        await database.HashDeleteAsync(key, messageIdsArray);
    }

    public async Task<IEnumerable<Message>> GetMessagesAsync(int userId)
    {
        var key = $"user:{userId}:messages";
        var messages = await database.HashGetAllAsync(key);
        return messages.Select(message => JsonConvert.DeserializeObject<Message>(message.Value)).ToArray();
    }
}
