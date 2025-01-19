using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ChatRepository(CoCreateDbContext context) : IChatRepository
{
    public async Task<Chat> CreateAsync(Chat chat)
    {
        await context.Chats.AddAsync(chat);
        await context.SaveChangesAsync();

        var created = await context
            .Chats.Include(c => c.ChatMemberships)
            .ThenInclude(cm => cm.User)
            .FirstOrDefaultAsync(c => c.Id == chat.Id);

        return created!;
    }
}
