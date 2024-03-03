using API.Factories;
using API.Models;
using Application.DTOs.AssetDTOs;
using Application.DTOs.ProjectRoleDTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ProjectRoleController : COCREATEAPIControllerBase
{
    private readonly IProjectRoleService projectRoleService;
    private readonly ICurrentUserContextService currentUserContextService;

    public ProjectRoleController(
        IProjectRoleService projectRoleService,
        ICurrentUserContextService currentUserContextService
    )
    {
        this.projectRoleService = projectRoleService;
        this.currentUserContextService = currentUserContextService;
    }

    [HttpPost]
    public async Task<ActionResult<APIResponse<ProjectRoleDTO>>> Create(
        ProjectRoleCreateDTO projectRoleCreateDTO
    )
    {
        var projectRole = await projectRoleService.CreateAsync(projectRoleCreateDTO);

        return Ok(APIResponseFactory.CreateSuccess(projectRole));
    }

    [HttpPut]
    public async Task<ActionResult<APIResponse<ProjectRoleDTO>>> Update(
        ProjectRoleUpdateDTO projectRoleUpdateDTO
    )
    {
        var projectRole = await projectRoleService.UpdateAsync(projectRoleUpdateDTO);

        return Ok(APIResponseFactory.CreateSuccess(projectRole));
    }
}
