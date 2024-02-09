namespace Application.DTOs.ProjectRoleDTOs;

public class ProjectRoleCreateWrapperDTO
{
    public required ProjectRoleCreateDTO ProjectRoleCreateDTO { get; set; }
    public required List<IFormFile> MediaFiles { get; set; }
}
