using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Services;

public class InMemoryMessageStorage : IMessageStorageService
{
    private readonly IMemoryCache memoryCache;

    public InMemoryMessageStorage(IMemoryCache memoryCache)
    {
        this.memoryCache = memoryCache;
    }

    public Task StoreMessageAsync(Guid key, string message)
    {
        memoryCache.Set(key, message, TimeSpan.FromMinutes(5));
        return Task.CompletedTask;
    }

    public Task<string> RetrieveMessageAsync(Guid key)
    {
        var message = memoryCache.Get<string>(key);

        if (message == null)
        {
            return Task.FromResult("");
        }

        return Task.FromResult(message);
    }

    public Task DeleteMessageAsync(Guid key)
    {
        memoryCache.Remove(key);
        return Task.CompletedTask;
    }
}
