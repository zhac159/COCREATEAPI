using Application.DTOs.AssetOfferDTOs;

namespace Application.Interfaces;

public interface IAssetOfferService
{
    Task<AssetOfferDTO> CreateAsync(AssetOfferCreateDTO assetCreateDTO);
}