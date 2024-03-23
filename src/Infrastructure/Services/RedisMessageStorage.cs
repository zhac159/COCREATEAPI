// namespace Infrastructure.Services;

// public class RedisMessageStorage : IMessageStorageService
// {
//     private readonly IRedisService redisService;

//     public RedisMessageStorage(IRedisService redisService)
//     {
//         this.redisService = redisService;
//     }

//     public async Task StoreMessageAsync(string key, string message)
//     {
//         var db = redisService.GetDatabase();
//         await db.StringSetAsync(key, message);
//     }

//     public async Task<string> RetrieveMessageAsync(string key)
//     {
//         var db = redisService.GetDatabase();

//         if (!db.KeyExists(key))
//         {
//             return "";
//         }

//         var result = await db.StringGetAsync(key);

//         return result.HasValue ? result.ToString() : "";
//     }

//     public async Task DeleteMessageAsync(string key)
//     {
//         var db = redisService.GetDatabase();
//         await db.KeyDeleteAsync(key);
//     }
// }
