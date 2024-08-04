using API.Factories;
using API.Models;
using Application.DTOs;
using Application.DTOs.UserDtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JwtInDotnetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserService userService;

        private readonly ILogger<LoginController> logger;
        private readonly IAuthenticationService authenticationService;

        public LoginController(
            IUserService userService,
            ILogger<LoginController> logger,
            IAuthenticationService authenticationService
        )
        {
            this.userService = userService;
            this.logger = logger;
            this.authenticationService = authenticationService;
        }

        [HttpPost]
        public async Task<ActionResult<APIResponse<LoginResponseDTO>>> Post(
            UserLoginDTO userLoginDTO
        )
        {
            var user = await userService.AuthenticateAsync(userLoginDTO);

            var token = authenticationService.CreateJWTTokenAsync(user);

            var response = new LoginResponseDTO { User = user, Token = token };

            return Ok(APIResponseFactory.CreateSuccess(response));
        }

        [HttpPost("register")]
        public async Task<ActionResult<APIResponse<LoginResponseDTO>>> Post(
            UserCreateDTO userCreateDTO
        )
        {
            var createdUser = await userService.CreateAsync(userCreateDTO);

            var token = authenticationService.CreateJWTTokenAsync(createdUser);

            var response = new LoginResponseDTO { User = createdUser, Token = token };

            return Ok(APIResponseFactory.CreateSuccess(response));
        }

        [HttpPost("token-login")]
        public async Task<ActionResult<APIResponse<LoginResponseDTO>>> Post(
            UserTokenLoginDTO tokenLoginDTO
        )
        {
            var userId = authenticationService.AuthenticateJWTTokenAndGetUserId(
                tokenLoginDTO.Token
            );

            var user = await userService.GetByIdAsync(userId);

            var token = authenticationService.CreateJWTTokenAsync(user);

            var response = new LoginResponseDTO { User = user, Token = token };

            return Ok(APIResponseFactory.CreateSuccess(response));
        }
    }
}
