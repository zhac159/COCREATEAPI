namespace Application.DTOs;

public class UserCreateDTO
{
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
    public required string Location { get; set; }
}