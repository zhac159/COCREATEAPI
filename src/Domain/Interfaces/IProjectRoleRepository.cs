using Domain.Entities;
using Domain.Queries;

namespace Domain.Interfaces;

public interface  IProjectRoleRepository
{
    Task<ProjectRole?> GetByIdAsync(int projectRoleId);
    Task<ProjectRole?> GetByIdIncludeProjectAsync(int projectRoleId);
    Task<ProjectRole> CreateAsync(ProjectRole projectRole);
    Task<ProjectRole> UpdateAsync(ProjectRole projectRole);
    Task<bool> DeleteAsync(ProjectRole projectRole);
    Task<List<(int, Project)>> GetMatchingProjectRoleIdsAsync(GetMatchingProjectRolesAsyncQuery query);
}