using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories;

public class PortofolioContentRepository : IPortofolioContentRepository
{
    private readonly CoCreateDbContext context;

    public PortofolioContentRepository(CoCreateDbContext context)
    {
        this.context = context;
    }

    public async Task<PortofolioContent> CreateAsync(PortofolioContent portofolioContent)
    {
        await context.PortofolioContents.AddAsync(portofolioContent);
        await context.SaveChangesAsync();

        return portofolioContent;
    }

    public async Task<PortofolioContent?> GetByIdAsync(int id)
    {
        return await context.PortofolioContents.FindAsync(id);
    }

    public async Task<bool> DeleteAsync(PortofolioContent portofolioContent)
    {
        context.PortofolioContents.Remove(portofolioContent);
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<PortofolioContent> UpdateAsync(PortofolioContent portofolioContent)
    {
        context.PortofolioContents.Update(portofolioContent);
        await context.SaveChangesAsync();

        return portofolioContent;
    }
}