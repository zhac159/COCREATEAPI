using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Profiles;

public class UserServiceProfile : Profile
{
    public UserServiceProfile()
    {
        CreateMap<UserDTO, User>();
        CreateMap<UserCreateDTO, User>();
        CreateMap<User, UserDTO>();
    }
}
