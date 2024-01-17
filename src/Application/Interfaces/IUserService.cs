using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces;

public interface IUserService
{
    Task<UserDTO> AuthenticateAsync(UserLoginDTO userLoginDTO);
    Task<UserDTO> CreateAsync(UserCreateDTO user);
}
