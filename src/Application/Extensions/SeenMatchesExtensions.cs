using Application.DTOs.SeenMatchesDTOs;
using Domain.Entities;

namespace Application.Extensions;

public static class SeenMatchesExtensions
{
    public static SeenMatchesDTO ToDTO(this SeenMatches seenMatches)
    {
        return new SeenMatchesDTO
        {
            ProjectRoleId = seenMatches.ProjectRoleId,
            UserId = seenMatches.UserId,
            Id = seenMatches.Id,
        };
    }
}
