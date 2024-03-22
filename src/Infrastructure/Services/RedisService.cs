namespace Infrastructure.Services;

using StackExchange.Redis;

public class RedisService : IRedisService
{
    private readonly ConnectionMultiplexer redis;

    public RedisService(string configuration)
    {
        redis = ConnectionMultiplexer.Connect(configuration);
    }

    public IDatabase GetDatabase()
    {
        return redis.GetDatabase();
    }
}