using System.ComponentModel.DataAnnotations;
using Domain.Entities;

namespace Application.DTOs.UserDtos;

public class UserCreateDTO
{
    [Required]
    public required string Username { get; set; }

    [Required]
    public required string Password { get; set; }

    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    public User ToEntity()
    {
        return new User
        {
            Username = Username,
            Password = Password,
            Email = Email,
        };
    }
}
