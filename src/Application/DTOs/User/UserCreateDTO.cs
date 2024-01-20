using System.ComponentModel.DataAnnotations;
using Domain.Entities;

namespace Application.DTOs.UserDtos;

public class UserCreateDTO
{
    public required string Username { get; set; }
    public required string Password { get; set; }
    
    [EmailAddress]
    public required string Email { get; set; }

    public required string Location { get; set; }

    public User ToEntity()
    {
        return new User
        {
            Username = Username,
            Password = Password,
            Email = Email,
            Location = Location
        };
    }
}
