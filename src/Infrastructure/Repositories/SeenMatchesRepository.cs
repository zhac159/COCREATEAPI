using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories;

public class SeenMatchesRepository : ISeenMatchesRepository
{
    private readonly CoCreateDbContext context;

    public SeenMatchesRepository(CoCreateDbContext context)
    {
        this.context = context;
    }

    public async Task<SeenMatches> CreateAsync(SeenMatches seenMatch)
    {
        await context.SeenMatches.AddAsync(seenMatch);
        await context.SaveChangesAsync();

        return seenMatch;
    }
}