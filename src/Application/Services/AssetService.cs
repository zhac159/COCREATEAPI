using System.Text;
using Application.DTOs.AssetDTOs;
using Application.Extensions;
using Application.Interfaces;
using Azure.Storage.Blobs;
using Domain.Interfaces;

namespace Application.Services;

public class AssetService : IAssetService
{
    private readonly IAssetRepository assetRepository;
    private readonly BlobServiceClient blobStorageService;

    public AssetService(IAssetRepository assetRepository, BlobServiceClient blobStorageService)
    {
        this.assetRepository = assetRepository;
        this.blobStorageService = blobStorageService;
    }

    public async Task<AssetDTO> CreateAsync(AssetCreateDTO assetCreateDTO, int userId)
    {
        var fileSrc = new StringBuilder()
            .Append("asset_")
            .Append(userId)
            .Append("_")
            .Append(assetCreateDTO.Name)
            .Append("_")
            .Append(Guid.NewGuid().ToString())
            .ToString();
        
        var asset = assetCreateDTO.ToEntity(fileSrc, userId);

        var createdAsset = await assetRepository.CreateAsync(asset);

        return createdAsset.ToDTO();
    }
}
