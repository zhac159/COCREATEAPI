using Application.DTOs.UserDtos;

namespace Application.Interfaces;

public interface IAuthenticationService
{
    string CreateJWTTokenAsync(UserDTO user);
    int AuthenticateJWTTokenAndGetUserId(string token);
}