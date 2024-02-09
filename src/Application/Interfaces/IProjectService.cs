using Application.DTOs.ProjectDTOs;

namespace Application.Interfaces;

public interface IProjectService
{
    Task<ProjectDTO> CreateAsync(ProjectCreateWrapperDTO projectCreateDTO, int userId);
    Task<ProjectDTO?> GetByIdAsync(int id);
}
