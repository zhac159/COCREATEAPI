using Application.DTOs.UserDtos;

namespace Application.Interfaces;

public interface IUserService
{
    Task<UserDTO> AuthenticateAsync(UserLoginDTO userLoginDTO);
    Task<UserDTO> CreateAsync(UserCreateDTO user);
    Task<UserDTO> GetByIdAsync(int id);
    Task<UserDTO> UpdateAsync(UserUpdateDTO userUpdateDTO, int userId);
}
