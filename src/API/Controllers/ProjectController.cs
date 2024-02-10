using API.Factories;
using API.Models;
using Application.DTOs.ProjectDTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ProjectController : COCREATEAPIControllerBase
{
    private readonly IProjectService projectService;
    private readonly ICurrentUserContextService currentUserContextService;

    public ProjectController(
        IProjectService projectService,
        ICurrentUserContextService currentUserContextService
    )
    {
        this.projectService = projectService;
        this.currentUserContextService = currentUserContextService;
    }

    [HttpPost]
    public async Task<ActionResult<APIResponse<ProjectDTO>>> Create(
        [FromForm] ProjectCreateWrapperDTO projectCreateWrapperDTO
    )
    {
        var project = await projectService.CreateAsync(
            projectCreateWrapperDTO,
            currentUserContextService.GetUserId()
        );

        return Ok(APIResponseFactory.CreateSuccess(project));
    }

    [AllowAnonymous]
    [HttpGet("{projectId:int}")]
    public async Task<ActionResult<APIResponse<ProjectDTO>>> GetById(int projectId)
    {
        var project = await projectService.GetByIdAsync(projectId);
        return Ok(APIResponseFactory.CreateSuccess(project));
    }
    
}