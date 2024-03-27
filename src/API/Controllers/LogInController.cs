using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Factories;
using API.Models;
using Application.DTOs;
using Application.DTOs.UserDtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace JwtInDotnetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration config;

        private readonly IUserService userService;

        private readonly ILogger<LoginController> logger;

        public LoginController(IConfiguration config, IUserService userService, ILogger<LoginController> logger)
        {
            this.config = config;
            this.userService = userService;
            this.logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<APIResponse<LoginResponseDTO>>> Post(
            UserLoginDTO userLoginDTO
        )
        {

            logger.LogError("LoginController -> Post -> userLoginDTO");
            
            var user = await userService.AuthenticateAsync(userLoginDTO);

            var claims = new[] { new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()), new Claim(JwtRegisteredClaimNames.Email, user.Email ) };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var Sectoken = new JwtSecurityToken(
                config["Jwt:Issuer"],
                config["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
            );

            var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);

            var response = new LoginResponseDTO { User = user, Token = token };

            return Ok(APIResponseFactory.CreateSuccess(response));
        }

        [HttpPost("register")]
        public async Task<ActionResult<APIResponse<LoginResponseDTO>>> Post(
            UserCreateDTO userCreateDTO
        )
        {
            var createdUser = await userService.CreateAsync(userCreateDTO);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, createdUser.UserId.ToString()),
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var Sectoken = new JwtSecurityToken(
                config["Jwt:Issuer"],
                config["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
            );

            var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);

            var response = new LoginResponseDTO { User = createdUser, Token = token };

            return Ok(APIResponseFactory.CreateSuccess(response));
        }
    }
}
