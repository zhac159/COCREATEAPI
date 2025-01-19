using Domain.Entities;

namespace Domain.Interfaces;

public interface IChatRepository
{
    Task<Chat> CreateAsync(Chat chat);
}
