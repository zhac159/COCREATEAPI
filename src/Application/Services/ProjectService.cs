using System.Text;
using Application.DTOs.ProjectDTOs;
using Application.Extensions;
using Application.Interfaces;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Services;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository projectRepository;
    private readonly IStorageService storageService;

    public ProjectService(IProjectRepository projectRepository, IStorageService storageService)
    {
        this.projectRepository = projectRepository;
        this.storageService = storageService;
    }

    public async Task<ProjectDTO> CreateAsync(
        ProjectCreateWrapperDTO projectCreateWrapperDTO,
        int userId
    )
    {
        var fileSrcs = new List<string>();

        if (
            projectCreateWrapperDTO.ProjectCreateDTO.Name == "null"
            || projectCreateWrapperDTO.ProjectCreateDTO.Name == "undefined"
        )
        {
            throw new InvalidModelException();
        }

        for (int i = 0; i < projectCreateWrapperDTO.MediaFiles.Count; i++)
        {
            var fileSrc = new StringBuilder()
                .Append("project_")
                .Append(userId)
                .Append("_")
                .Append(projectCreateWrapperDTO.ProjectCreateDTO.Name)
                .Append("_")
                .Append(Guid.NewGuid().ToString())
                .Append(".jpeg")
                .ToString();

            await storageService.UploadFile(
                fileSrc,
                projectCreateWrapperDTO.MediaFiles[i],
                "projects"
            );

            fileSrcs.Add(fileSrc);
        }

        var project = projectCreateWrapperDTO.ProjectCreateDTO.ToEntity(fileSrcs, userId);

        var createdProject = await projectRepository.CreateAsync(project);

        return createdProject.ToDTO();
    }

    public async Task<ProjectDTO?> GetByIdAsync(int id)
    {
        var project = await projectRepository.GetByIdIncludeAllPropertiesAsync(id);

        if (project is null)
        {
            throw new EntityNotFoundException();
        }

        return project.ToDTO();
    }
}
