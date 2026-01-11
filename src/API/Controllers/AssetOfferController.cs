using API.Factories;
using API.Models;
using Application.DTOs.AssetOfferDTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AssetOfferController : COCREATEAPIControllerBase
{
    private readonly IAssetOfferService assetOfferService;

    public AssetOfferController(IAssetOfferService assetOfferService)
    {
        this.assetOfferService = assetOfferService;
    }

    [HttpPost("create")]
    public async Task<ActionResult<APIResponse<AssetOfferDTO>>> Create(
        AssetOfferCreateDTO assetOfferCreateDTO
    )
    {
        var assetOffer = await assetOfferService.CreateAsync(assetOfferCreateDTO);

        return Ok(APIResponseFactory.CreateSuccess(assetOffer));
    }
}
