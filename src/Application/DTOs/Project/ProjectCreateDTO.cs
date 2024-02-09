using Domain.Entities;

namespace Application.DTOs.ProjectDTO;

public class ProjectCreateDTO
{
    public required string Name { get; set; }
    public required string Description { get; set; }

    public Project ToEntity(List<string> fileSrcs, int projectManagerId)
    {
        return new Project
        {
            Name = Name,
            Description = Description,
            FileSrcs = fileSrcs,
            ProjectManagerId = projectManagerId,
        };
    }
}