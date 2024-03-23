using Domain.Entities;

public interface IMessageStorageService
{
    Task StoreMessageAsync(int userId, EnquiryMessage enquiryMessage);
    // Task<string> RetrieveMessageAsync(string key);
    Task DeleteMessageAsync(int userId, Guid messageId);
}