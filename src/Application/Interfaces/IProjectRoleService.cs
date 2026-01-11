using Application.DTOs.ProjectRoleDTOs;

namespace Application.Interfaces;

public interface IProjectRoleService
{
    Task<ProjectRoleDTO> CreateAsync(ProjectRoleCreateDTO projectCreateDTO);
    Task<ProjectRoleDTO> GetAsync(int id);
    Task<ProjectRoleDTO> UpdateAsync(ProjectRoleUpdateDTO projectUpdateDTO);
    Task<bool> CompleteAsync(ProjectRoleCompleteDTO projectRoleCompleteDTO);
}
