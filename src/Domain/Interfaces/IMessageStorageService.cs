public interface IMessageStorageService
{
    Task StoreMessageAsync(Guid key, string message);
    Task<string> RetrieveMessageAsync(Guid key);
    Task DeleteMessageAsync(Guid key);
}