using Application.DTOs.AssetDTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;


public class AssetController : COCREATEAPIControllerBase
{
    private readonly IAssetService assetService;

    private readonly ICurrentUserContextService currentUserContextService;

    public AssetController(IAssetService assetService, ICurrentUserContextService currentUserContextService)
    {
        this.assetService = assetService;
        this.currentUserContextService = currentUserContextService;

    }

    [HttpPost]
public async Task<IActionResult> Create(AssetCreateWrapperDTO  assetCreateWrapperDTO)
    {
        var asset = await assetService.CreateAsync(assetCreateWrapperDTO, currentUserContextService.GetUserId());

        return Ok(asset);
    }
} 