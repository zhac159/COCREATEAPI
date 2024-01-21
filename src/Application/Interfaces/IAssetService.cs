using Application.DTOs.AssetDTOs;

namespace Application.Interfaces;

public interface IAssetService
{
    Task<AssetDTO> CreateAsync(AssetCreateDTO assetCreateDTO, int userId);
}