using Application.DTOs.ProjectRoleDTOs;

namespace Application.Interfaces;

public interface IProjectRoleService
{
    Task<ProjectRoleDTO> CreateAsync(ProjectRoleCreateDTO projectCreateDTO);
    Task<ProjectRoleDTO> UpdateAsync(ProjectRoleUpdateDTO projectUpdateDTO);
}
