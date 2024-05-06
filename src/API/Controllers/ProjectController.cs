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
        ProjectCreateDTO projectCreateDTO
    )
    {
        var project = await projectService.CreateAsync(projectCreateDTO);

        return Ok(APIResponseFactory.CreateSuccess(project));
    }

    [HttpPost("complete")]
    public async Task<ActionResult<APIResponse<bool>>> Complete(
        ProjectCompleteDTO projectCompleteDTO
    )
    {
        var success = await projectService.CompleteAsync(projectCompleteDTO);

        return Ok(APIResponseFactory.CreateSuccess(success));
    }

    [AllowAnonymous]
    [HttpGet("{projectId:int}")]
    public async Task<ActionResult<APIResponse<ProjectDTO>>> GetById(int projectId)
    {
        var project = await projectService.GetByIdAsync(projectId);
        return Ok(APIResponseFactory.CreateSuccess(project));
    }
}
