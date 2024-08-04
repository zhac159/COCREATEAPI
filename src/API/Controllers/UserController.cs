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
    public async Task<ActionResult<APIResponse<UserPortofolioDTO>>> UpdatePortofolio(
        UserPortofolioUpdateDTO userPortofolioUpdateDTO
    )
    {
        var updatedPortfolio = await userService.UpdatePortofolio(userPortofolioUpdateDTO);

        return Ok(APIResponseFactory.CreateSuccess(updatedPortfolio));
    }

    [HttpPut("public-key")]
    public async Task<ActionResult<APIResponse<bool>>> UpdatePublicKey(
        UserPublicKeyUpdateDTO userPublicKeyUpdateDTO
    )
    {
        var successfull = await userService.UpdatePublicKeyAsync(userPublicKeyUpdateDTO);

        return Ok(APIResponseFactory.CreateSuccess(successfull));
    }

    [AllowAnonymous]
    [HttpPost("profiles")]
    public async Task<ActionResult<APIResponse<UserProfilesDTO>>> GetProfiles(
        UserGetProfilesDTO userGetProfilesDTO
    )
    {
        var profiles = await userService.GetUserProfilesAsync(userGetProfilesDTO);

        return Ok(APIResponseFactory.CreateSuccess(profiles));
    }

    [HttpGet("get-profile")]
    public async Task<ActionResult<APIResponse<UserProfileDTO>>> GetProfile(int userId)
    {
        var profile = await userService.GetUserProfileAsync(userId);

        return Ok(APIResponseFactory.CreateSuccess(profile));
    }

    [HttpPost("verify-email")]
    public async Task<ActionResult<APIResponse<bool>>> VerifyEmail(
        UserVerifyEmailDTO userVerifyEmailDTO
    )
    {
        var successfull = await userService.VerifyEmailAsync(userVerifyEmailDTO);

        return Ok(APIResponseFactory.CreateSuccess(successfull));
    }

    [HttpPost("resend-verification-email")]
    public async Task<ActionResult<APIResponse<bool>>> ResendVerificationEmail()
    {
        await userService.ResendVerificationEmailAsync();

        return Ok(APIResponseFactory.CreateSuccess(true));
    }

    [HttpPost("update-email")]
    public async Task<ActionResult<APIResponse<bool>>> UpdateAndVerifyEmail(
        UserUpdateEmailDTO userUpdateEmailDTO
    )
    {
        await userService.UpdateAndVerifyEmailAsync(userUpdateEmailDTO);

        return Ok(APIResponseFactory.CreateSuccess(true));
    }

    [HttpPost("change-password")]
    public async Task<ActionResult<APIResponse<bool>>> ChangePassword(
        UserChangePasswordDTO userChangePasswordDTO
    )
    {
        var successfull = await userService.ChangePasswordAsync(userChangePasswordDTO);

        return Ok(APIResponseFactory.CreateSuccess(successfull));
    }
    
    [HttpPost("authenticate-token")]
    public async Task<ActionResult<APIResponse<UserDTO>>> GetAuthenticatedUser()
    {
        var user = await userService.GetByIdAsync(currentUserContextService.GetUserId());

        return Ok(APIResponseFactory.CreateSuccess(user));
    }

    [HttpDelete("account")]
    public async Task<ActionResult<APIResponse<bool>>> DeleteAccount()
    {
        var successfull = await userService.DeleteAsync();

        return Ok(APIResponseFactory.CreateSuccess(successfull));
    }
}
