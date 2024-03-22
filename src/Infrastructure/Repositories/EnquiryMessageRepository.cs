using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories;

public class EnquiryMessageRepository : IEnquiryMessageRepository
{
    private readonly CoCreateDbContext context;

    public EnquiryMessageRepository(CoCreateDbContext context)
    {
        this.context = context;
    }

    public async Task<EnquiryMessage> CreateAsync(EnquiryMessage enquiryMessage)
    {
        await context.EnquiryMessages.AddAsync(enquiryMessage);
        await context.SaveChangesAsync();

        return enquiryMessage;
    }

    public async Task<EnquiryMessage?> GetByIdAsync(int id)
    {
        return await context.EnquiryMessages.FindAsync(id);
    }

    public async Task<bool> DeleteAsync(EnquiryMessage enquiryMessage)
    {
        context.EnquiryMessages.Remove(enquiryMessage);
        await context.SaveChangesAsync();

        return true;
    }
}
