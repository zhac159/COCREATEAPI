namespace Infrastructure.Services;

public class RedisMessageStorage : IMessageStorageService
{
    private readonly IRedisService redisService;

    public RedisMessageStorage(IRedisService redisService)
    {
        this.redisService = redisService;
    }

    public async Task StoreMessageAsync(Guid key, string message)
    {
        var db = redisService.GetDatabase();
        await db.StringSetAsync(key.ToString(), message);
    }

    public async Task<string> RetrieveMessageAsync(Guid key)
    {
        var db = redisService.GetDatabase();

        if (!db.KeyExists(key.ToString()))
        {
            return "";
        }

        var result = await db.StringGetAsync(key.ToString());

        return result.HasValue ? result.ToString() : "";
    }

    public async Task DeleteMessageAsync(Guid key)
    {
        var db = redisService.GetDatabase();
        await db.KeyDeleteAsync(key.ToString());
    }
}
