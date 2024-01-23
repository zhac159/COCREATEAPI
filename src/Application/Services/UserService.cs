using Application.DTOs.UserDtos;
using Application.Extensions;
using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository userRepository;

    public UserService(IUserRepository userRepository, IStorageService storageService)
    {
        this.userRepository = userRepository;
    }

    public async Task<UserDTO> AuthenticateAsync(UserLoginDTO userLoginDTO)
    {

        var user = await userRepository.GetByUsernameAsync(userLoginDTO.Username);

        if (user is null)
        {
            throw new EntityNotFoundException();
        }

        if (user.Password != userLoginDTO.Password)
        {
            throw new InvalidPasswordException();
        }

        return user.ToDTO();
    }

    public async Task<UserDTO> CreateAsync(UserCreateDTO userCreateDTO)
    {
        var user = userCreateDTO.ToEntity();

        if (await userRepository.GetByUsernameAsync(user.Username) is not null)
        {
            throw new EntityAlreadyExistsException();
        }

        var createdUser = await userRepository.CreateAsync(user);

        return createdUser.ToDTO();
    }

    public async Task<UserDTO> GetByIdAsync(int id)
    {
        var user = await userRepository.GetByIdAsync(id);

        if (user is null)
        {
            throw new EntityNotFoundException();
        }

        return user.ToDTO();
    }

    public async Task<UserDTO> UpdateAsync(UserUpdateDTO userUpdateDTO, int userId)
    {

        var user = await userRepository.GetByIdAsync(userId);

        if (user is null)
        {
            throw new EntityNotFoundException();
        }

        user.UpdateFromDTO(userUpdateDTO);

        var updatedUser = await userRepository.UpdateAsync(user);

        return updatedUser.ToDTO();
    }

}