using Domain.Entities;
using Domain.Enums;

namespace Domain.Interfaces;

public interface IAssetRepository
{
    Task<Asset?> GetByIdIncludeAllPropertiesAsync(int id);
    Task<List<Asset>> FindFirstMatchingAssetsAsync(string searchTerm, AssetType? assetType);
    Task<Asset> CreateAsync(Asset asset);
    Task<Asset> UpdateAsync(Asset asset);
    Task<bool> DeleteAsync(Asset asset);
}
