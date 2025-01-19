using Application.DTOs.Chat;
using Application.DTOs.ProjectDTOs;

namespace Application.Interfaces;

public interface IProjectService
{
    Task<EntityWithChatDTO<ProjectDTO>> CreateAsync(ProjectCreateDTO projectCreateDTO);
    Task<ProjectDTO> UpdateAsync(ProjectUpdateDTO projectUpdateDTO);
    Task<ProjectDTO?> GetByIdAsync(int id);
    Task<bool> CompleteAsync(ProjectCompleteDTO projectCompleteDTO);
    Task<ProjectCompletedDTO> GetCompletedProjectByIdAsync(int id);
    Task<ProjectDTO?> GetProjectByRoleIdAsync(int roleId);
}
