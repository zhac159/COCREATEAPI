using API.Models;
using Application.DTOs.UserDtos;
using Application.Interfaces;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService userService;

    private readonly ICurrentUserContextService currentUserContextService;

    public UserController(IUserService userService, ICurrentUserContextService currentUserContextService)
    {
        this.userService = userService;
        this.currentUserContextService = currentUserContextService;
    }
    
    [HttpGet("{userId:int}")]
    public async Task<ActionResult<APIResponse<UserDTO>>> Post(int userId)
    {
        var user = await userService.GetByIdAsync(userId);
        return Ok(user);
    }

    [HttpPut()]
    public async Task<ActionResult<APIResponse<UserDTO>>> Put(UserUpdateDTO userUpdateDTO)
    {
        var user = await userService.UpdateAsync(userUpdateDTO, currentUserContextService.GetUserId());
        return Ok(user);
    }
}
