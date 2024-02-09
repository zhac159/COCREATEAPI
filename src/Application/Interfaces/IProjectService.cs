using Application.DTOs.ProjectDTO;

namespace Application.Interfaces;

public interface IProjectService
{
    Task<ProjectDTO> CreateAsync(ProjectCreateWrapperDTO projectCreateDTO, int userId);
}
