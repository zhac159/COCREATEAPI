using Application.DTOs.ProjectDTOs;
using Application.DTOs.SkillDTOs;
using Application.DTOs.UserDtos;
using Application.Extensions;
using Application.Interfaces;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Queries;
using NetTopologySuite.Geometries;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository userRepository;
    private readonly IProjectRoleRepository projectRoleRepository;
    private readonly IStorageService storageService;
    private readonly ICurrentUserContextService currentUserContextService;
    private readonly IChatHubService chatHubService;

    public UserService(
        IUserRepository userRepository,
        IStorageService storageService,
        IProjectRoleRepository projectRoleRepository,
        ICurrentUserContextService currentUserContextService,
        IChatHubService chatHubService
    )
    {
        this.userRepository = userRepository;
        this.projectRoleRepository = projectRoleRepository;
        this.storageService = storageService;
        this.currentUserContextService = currentUserContextService;
        this.chatHubService = chatHubService;
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
        var user = await userRepository.GetByIdIncludeAllPropertiesAsync(id);

        if (user is null)
        {
            throw new EntityNotFoundException();
        }

        return user.ToDTO();
    }

    public async Task<UserDTO> UpdateAsync(UserUpdateDTO userUpdateDTO, int userId)
    {
        var user = await userRepository.GetByIdIncludeAllPropertiesAsync(userId);

        if (user is null)
        {
            throw new EntityNotFoundException();
        }

        user.UpdateFromDTO(userUpdateDTO);

        var updatedUser = await userRepository.UpdateAsync(user);

        return updatedUser.ToDTO();
    }

    public async Task<List<SkillDTO>> UpdateSkillsAsync(
        List<SkillUpdateDTO> skillUpdateDTOs,
        int userId
    )
    {
        var user = await userRepository.GetByIdIncludeSkillsAsync(userId);

        if (user is null)
        {
            throw new EntityNotFoundException();
        }

        user.UpdateSkillsFromDTO(skillUpdateDTOs);

        var updatedUser = await userRepository.UpdateAsync(user);

        if (updatedUser.Skills is null)
        {
            return new List<SkillDTO>();
        }

        return updatedUser.Skills.Select(s => s.ToDTO()).ToList();
    }

    public async Task<UserLocationDTO> UpdateLocationAsync(
        UserLocationUpdateDTO userLocationUpdateDTO,
        int userId
    )
    {
        var user = await userRepository.GetByIdIncludeAllPropertiesAsync(userId);

        if (user is null)
        {
            throw new EntityNotFoundException();
        }

        user.Location = new Point(userLocationUpdateDTO.Longitude, userLocationUpdateDTO.Latitude)
        {
            SRID = 4326
        };
        user.Address = userLocationUpdateDTO.Address;

        var updatedUser = await userRepository.UpdateAsync(user);

        return updatedUser.ToLocationDTO();
    }

    public async Task<ProjectWithMatchingRolesListDTO> GetMatchingProjectRolesAsync(
        UserGetMatchingProjectRolesDTO userGetMatchingProjectRolesDTO,
        int userId
    )
    {
        var user = await userRepository.GetByIdIncludeSkillsAsync(userId);

        if (user is null)
        {
            throw new EntityNotFoundException();
        }

        var userSkillTypes = user.Skills.Select(s => s.SkillType).ToList();

        if (user.Location is null)
        {
            throw new LocationNotSetException();
        }

        var getMatchingProjectRolesAsyncQuery = new GetMatchingProjectRolesAsyncQuery
        {
            Location = user.Location,
            Distance = userGetMatchingProjectRolesDTO.Distance,
            SkillTypes = userSkillTypes,
            Effort = userGetMatchingProjectRolesDTO.Effort,
            UserId = userId,
        };

        var matchingProjects = await projectRoleRepository.GetMatchingProjectRoleIdsAsync(
            getMatchingProjectRolesAsyncQuery
        );
        var matchingProjectDTOs = matchingProjects
            .Select(
                mp =>
                    new ProjectWithMatchingRoleDTO
                    {
                        ProjectRoleId = mp.Item1,
                        Project = mp.Item2.ToDTO(),
                    }
            )
            .ToList();

        var result = new ProjectWithMatchingRolesListDTO
        {
            ProjectWithMatchingRoles = matchingProjectDTOs
        };

        return result;
    }

    public async Task<UserPortofolioDTO> UpdatePortofolio(
        UserPortofolioUpdateDTO userPortofolioUpdateDTO
    )
    {
        var user = await userRepository.GetByIdIncludePortofolioAsync(
            currentUserContextService.GetUserId()
        );

        if (user is null)
        {
            throw new EntityNotFoundException();
        }

        await user.UpdatePortofolioFromDTOAsync(userPortofolioUpdateDTO, storageService);

        var updatedUser = await userRepository.UpdateAsync(user);

        return updatedUser.ToPortofolioDTO();
    }
}
