using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories;

public class AssetRepository : IAssetRepository
{
    private readonly CoCreateDbContext context;
    private readonly IStorageService storageService;

    public AssetRepository(CoCreateDbContext context, IStorageService storageService)
    {
        this.context = context;
        this.storageService = storageService;
    }

    public async Task<Asset> CreateAsync(Asset asset)
    {
        await context.Assets.AddAsync(asset);
        await context.SaveChangesAsync();

        asset.Uris = asset.FileSrcs.Select(fileSrc => storageService.GetFileUri(fileSrc, "assets")).ToList();

        return asset;
    }

    public async Task<Asset?> GetByIdAsync(int id)
    {
        return await context.Assets.FindAsync(id);
    }

    public async Task<bool> DeleteAsync(Asset asset)
    {
        context.Assets.Remove(asset);
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<Asset> UpdateAsync(Asset asset)
    {
        context.Assets.Update(asset);
        await context.SaveChangesAsync();

        return asset;
    }
}