using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories;

public class ExperienceRepository : IExperienceRepository
{
    private readonly CoCreateDbContext context;

    public ExperienceRepository(CoCreateDbContext context)
    {
        this.context = context;
    }

    public async Task<Experience> CreateAsync(Experience experience)
    {
        await context.Experiences.AddAsync(experience);

        await context.SaveChangesAsync();

        return experience;
    } 
}
