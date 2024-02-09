using Domain.Entities;
using Domain.Queries;

namespace Domain.Interfaces;

public interface IProjectRoleRepository
{
    Task<ProjectRole?> GetByIdAsync(int id);
    Task<ProjectRole> CreateAsync(ProjectRole projectRole);
    Task<ProjectRole> UpdateAsync(ProjectRole projectRole);
    Task<bool> DeleteAsync(ProjectRole projectRole);
    Task<List<(int, Project)>> GetMatchingProjectRoleIdsAsync(GetMatchingProjectRolesAsyncQuery query);
}