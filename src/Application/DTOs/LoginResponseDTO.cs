using Application.DTOs.UserDtos;

namespace Application.DTOs;

public class LoginResponseDTO
{
    public required string Token { get; set; }
    public required UserDTO User { get; set; }
}
