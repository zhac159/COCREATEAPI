using Domain.Entities;

namespace Domain.Interfaces;

public interface IProjectRepository
{
    Task<Project?> GetByIdAsync(int id);
    Task<Project?> GetByIdIncludeAllPropertiesAsync(int id);
    Task<Project?> GetByIdIncludeAllExperiencesAsync(int id);
    Task<Project?> CreateAsync(Project project);
    Task<Project> UpdateAsync(Project project);
    Task<Project?> GetProjectByRoleIdAsync(int roleId);
    Task<bool> DeleteAsync(Project project);
}
