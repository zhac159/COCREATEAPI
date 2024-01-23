using Application.DTOs.AssetDTOs;

namespace Application.Interfaces;

public interface IAssetService
{
    Task<AssetDTO> CreateAsync(AssetCreateWrapperDTO assetCreateWrapperDTO, int userId);
    Task<bool> DeleteAsync(int id, int userId); 
    Task<AssetDTO> UpdateAsync(AssetUpdateWrapperDTO assetUpdateWrapperDTO, int userId);
}