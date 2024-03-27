using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

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

    public async Task<PortofolioContent?> GetByIdIncludeAllPropertiesAsync(int id)
    {
        return await context
            .PortofolioContents.Include(e => e.Medias)
            .FirstOrDefaultAsync(e => e.Id == id);
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
