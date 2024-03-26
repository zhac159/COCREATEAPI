
using StackExchange.Redis;

namespace Domain.Interfaces;

public interface IRedisService
{
    IDatabase GetDatabase();
}