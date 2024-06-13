using Application.DTOs.AssetOfferDTOs;
using Application.Extensions;
using Application.Interfaces;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Services;

public class AssetOfferService : IAssetOfferService
{
    private readonly IAssetOfferRepository assetOfferRepository;
    private readonly IProjectRepository projectRepository;
    private readonly ICurrentUserContextService currentUserContextService;

    public AssetOfferService(
        IAssetOfferRepository assetOfferRepository,
        IProjectRepository projectRepository,
        ICurrentUserContextService currentUserContextService 
    )
    {
        this.assetOfferRepository = assetOfferRepository;
        this.projectRepository = projectRepository;
        this.currentUserContextService = currentUserContextService;
    }

    public async Task<AssetOfferDTO> CreateAsync(AssetOfferCreateDTO assetOfferCreateDTO)
    {
        var project =
            await projectRepository.GetByIdAsync(assetOfferCreateDTO.ProjectId)
            ?? throw new ProjectNotExistException();

        if (project.ProjectManagerId != currentUserContextService.GetUserId())
        {
            throw new UnauthorizedAccessException();
        }

        var assetOffer = assetOfferCreateDTO.ToEntity();

        var createdAssetOffer = await assetOfferRepository.CreateAsync(assetOffer);

        if (createdAssetOffer == null)
        {
            throw new EntityNotFoundException();
        }
        

        return createdAssetOffer!.ToDTO();
    }
}
