// using Domain.Entities;
// using Microsoft.Extensions.Caching.Memory;

// namespace Infrastructure.Services;

// public class InMemoryMessageStorage : IMessageStorageService
// {
//     private readonly IMemoryCache memoryCache;

//     public InMemoryMessageStorage(IMemoryCache memoryCache)
//     {
//         this.memoryCache = memoryCache;
//     }

//     public Task StoreMessageAsync(int userId, EnquiryMessage enquiryMessage)
//     {
//         var messages = memoryCache.Get<Dictionary<Guid, EnquiryMessage>>(userId) ?? new Dictionary<Guid, EnquiryMessage>();
//         messages[enquiryMessage.Id] = enquiryMessage;
//         memoryCache.Set(userId, messages, TimeSpan.FromMinutes(5));
//         return Task.CompletedTask;
//     }

//     // public Task<string> RetrieveMessageAsync(string userKey, string messageKey)
//     // {
//     //     var messages = memoryCache.Get<Dictionary<string, string>>(userKey);
//     //     if (messages == null || !messages.TryGetValue(messageKey, out var message))
//     //     {
//     //         return Task.FromResult("");
//     //     }

//     //     return Task.FromResult(message);
//     // }

//     public Task DeleteMessageAsync(int userId, Guid messageId)
//     {
//         var messages = memoryCache.Get<Dictionary<Guid, EnquiryMessage>>(userId);
//         if (messages != null)
//         {
//             messages.Remove(messageId);
//             memoryCache.Set(messageId, messages, TimeSpan.FromMinutes(5));
//         }
//         return Task.CompletedTask;
//     }
// }


// // public async Task Acknowledge(int messageId)
// // {
// //     if (Context.User?.Identity?.Name == null)
// //     {
// //         throw new UnauthorizedAccessException("User is not authenticated");
// //     }

// //     var userName = Context.User.Identity.Name; // Get the name of the authenticated user

// //     await enquiryMessageRepository.DeleteAsyncById(messageId);
// // }