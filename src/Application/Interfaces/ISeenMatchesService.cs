using Application.DTOs.SeenMatchesDTOs;

namespace Application.Interfaces;

public interface ISeenMatchesService
{
    Task<SeenMatchesDTO> CreateAsync(SeenMatchesCreateDTO seenMatch);
}