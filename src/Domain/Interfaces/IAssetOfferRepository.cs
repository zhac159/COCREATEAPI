using Domain.Entities;

namespace Domain.Interfaces;

public interface IAssetOfferRepository
{
   Task<AssetOffer?> CreateAsync(AssetOffer assetOffer);
}