using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository userRepository;
    private readonly IMapper mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        this.userRepository = userRepository;
        this.mapper = mapper;
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

        return mapper.Map<UserDTO>(user);
    }

    public async Task<UserDTO> CreateAsync(UserCreateDTO userCreateDTO)
    {
        var user = mapper.Map<User>(userCreateDTO);

        if (await userRepository.GetByUsernameAsync(user.Username) is not null)
        {
            throw new EntityAlreadyExistsException();
        }

        var createdUser = await userRepository.CreateAsync(user);

        return mapper.Map<UserDTO>(createdUser);
    }

}