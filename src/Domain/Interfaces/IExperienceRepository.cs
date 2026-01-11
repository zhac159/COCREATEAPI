using Domain.Entities;

namespace Domain.Interfaces;

public interface IExperienceRepository
{
    Task<Experience> CreateAsync(Experience experience);
    // Task<Experience?> GetByIdIncludeAllPropertiesAsync(int id);
    // Task<bool> DeleteAsync(Experience experience);
    // Task<Experience> UpdateAsync(Experience experience);
}