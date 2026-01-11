using Domain.Entities;

namespace Application.DTOs.SeenMatchesDTOs;

public class SeenMatchesCreateDTO
{
    public int ProjectRoleId { get; set; }

    public SeenMatches ToEntity(int userId)
    {
        return new SeenMatches
        {
            ProjectRoleId = ProjectRoleId,
            UserId = userId
        };
    }
}