using Application.DTOs.SeenMatchesDTOs;
using Application.Interfaces;
using Application.Extensions;
using Domain.Interfaces;

namespace Application.Services;

public class SeenMatchesService : ISeenMatchesService
{
    private readonly ISeenMatchesRepository seenMatchesRepository;
    private readonly ICurrentUserContextService currentUserContextService;

    public SeenMatchesService(ISeenMatchesRepository seenMatchesRepository, ICurrentUserContextService currentUserContextService)
    {
        this.seenMatchesRepository = seenMatchesRepository;
        this.currentUserContextService = currentUserContextService;
    }

    public async Task<SeenMatchesDTO> CreateAsync(SeenMatchesCreateDTO seenMatch)
    {

        var seenMatchEntity = seenMatch.ToEntity(currentUserContextService.GetUserId());

        var createdSeenMatch = await seenMatchesRepository.CreateAsync(seenMatchEntity);

        return createdSeenMatch.ToDTO();
    }
}