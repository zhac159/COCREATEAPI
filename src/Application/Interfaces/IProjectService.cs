using Application.DTOs.ProjectDTOs;

namespace Application.Interfaces;

public interface IProjectService
{
    Task<ProjectDTO> CreateAsync(ProjectCreateDTO projectCreateDTO);
    Task<ProjectDTO?> GetByIdAsync(int id);
}
