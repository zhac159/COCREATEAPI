using Application.DTOs.ProjectRoleDTOs;

namespace Application.Interfaces;

public interface IProjectRoleService
{
    Task<ProjectRoleDTO> CreateAsync(ProjectRoleCreateWrapperDTO projectCreateDTO, int userId);
}
