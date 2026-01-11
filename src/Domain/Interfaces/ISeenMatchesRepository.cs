using Domain.Entities;

namespace Domain.Interfaces;

public interface ISeenMatchesRepository
{
    Task<SeenMatches> CreateAsync(SeenMatches seenMatch);
}