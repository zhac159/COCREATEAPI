using API.Factories;
using API.Models;
using Application.DTOs.ProjectDTOs;
using Application.DTOs.SkillDTOs;
using Application.DTOs.UserDtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class UserController : COCREATEAPIControllerBase
{
    private readonly IUserService userService;

    private readonly ICurrentUserContextService currentUserContextService;

    public UserController(
        IUserService userService,
        ICurrentUserContextService currentUserContextService
    )
    {
        this.userService = userService;
        this.currentUserContextService = currentUserContextService;
    }

    [AllowAnonymous]
    [HttpGet("{userId:int}")]
    public async Task<ActionResult<APIResponse<UserDTO>>> Post(int userId)
    {
        var user = await userService.GetByIdAsync(userId);
        return Ok(APIResponseFactory.CreateSuccess(user));
    }

    [HttpPut()]
    public async Task<ActionResult<APIResponse<UserDTO>>> Update(UserUpdateDTO userUpdateDTO)
    {
        var user = await userService.UpdateAsync(
            userUpdateDTO,
            currentUserContextService.GetUserId()
        );
        return Ok(APIResponseFactory.CreateSuccess(user));
    }

    [HttpPut("skills")]
    public async Task<ActionResult<APIResponse<SkillDTO>>> UpdateSkills(
        List<SkillUpdateDTO> skillUpdateDTO
    )
    {
        var skills = await userService.UpdateSkillsAsync(
            skillUpdateDTO,
            currentUserContextService.GetUserId()
        );
        return Ok(APIResponseFactory.CreateSuccess(skills));
    }

    [HttpPut("location")]
    public async Task<ActionResult<APIResponse<UserLocationDTO>>> UpdateLocation(
        UserLocationUpdateDTO locationUpdateDTO
    )
    {
        var successfull = await userService.UpdateLocationAsync(
            locationUpdateDTO,
            currentUserContextService.GetUserId()
        );

        return Ok(APIResponseFactory.CreateSuccess(successfull));
    }

    [HttpPost("matching-projects")]
    public async Task<
        ActionResult<APIResponse<ProjectWithMatchingRolesListDTO>>
    > GetMatchingProjectRoles(UserGetMatchingProjectRolesDTO userGetMatchingProjectRolesDTO)
    {
        var matchingProjects = await userService.GetMatchingProjectRolesAsync(
            userGetMatchingProjectRolesDTO,
            currentUserContextService.GetUserId()
        );

        return Ok(APIResponseFactory.CreateSuccess(matchingProjects));
    }

    [HttpPut("portofolio")]
        public async Task<
        ActionResult<APIResponse<UserPortofolioDTO>>
    > UpdatePortofolio(UserPortofolioUpdateDTO userPortofolioUpdateDTO)
    {
        var updatedPortfolio = await userService.UpdatePortofolio(
            userPortofolioUpdateDTO
        );

        return Ok(APIResponseFactory.CreateSuccess(updatedPortfolio));
    }
}
