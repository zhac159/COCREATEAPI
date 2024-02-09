namespace Application.DTOs.ProjectDTOs;

public class ProjectCreateWrapperDTO
{
    public required ProjectCreateDTO ProjectCreateDTO { get; set; }
    public required List<IFormFile> MediaFiles { get; set; }
}