using Application.DTOs.EnquiryDTOs;
using Application.DTOs.ProjectDTOs;
using Application.DTOs.SkillDTOs;
using Application.DTOs.UserDtos;

namespace Application.Interfaces;

public interface IUserService
{
    Task<UserDTO> AuthenticateAsync(UserLoginDTO userLoginDTO);
    Task<UserDTO> CreateAsync(UserCreateDTO user);
    Task<UserDTO> GetByIdAsync(int id);
    Task<UserDTO> UpdateAsync(UserUpdateDTO userUpdateDTO, int userId);
    Task<List<SkillDTO>> UpdateSkillsAsync(List<SkillUpdateDTO> userUpdateDTO, int userId);
    Task<UserLocationDTO> UpdateLocationAsync(UserLocationUpdateDTO location, int userId);
    Task<List<ProjectWithMatchingRoleDTO>> GetMatchingProjectRolesAsync(UserGetMatchingProjectRolesDTO userGetMatchingProjectRolesDTO, int userId);
}
