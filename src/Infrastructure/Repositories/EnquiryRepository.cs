using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories;

public class EnquiryRepository : IEnquiryRepository
{
    private readonly CoCreateDbContext context;

    public EnquiryRepository(CoCreateDbContext context)
    {
        this.context = context;
    }

    public async Task<Enquiry> CreateAsync(Enquiry enquiry)
    {
        await context.Enquiries.AddAsync(enquiry);
        await context.SaveChangesAsync();

        return enquiry;
    }

    public async Task<bool> DeleteAsync(Enquiry enquiry)
    {
        context.Enquiries.Remove(enquiry);
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<Enquiry?> GetByIdAsync(int id)
    {
        return await context.Enquiries.FindAsync(id);
    }

    public async Task<Enquiry> UpdateAsync(Enquiry enquiry)
    {
        context.Enquiries.Update(enquiry);
        await context.SaveChangesAsync();

        return enquiry;
    }
}