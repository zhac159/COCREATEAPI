using API.Factories;
using API.Models;
using Application.DTOs.AssetDTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AssetController : COCREATEAPIControllerBase
{
    private readonly IAssetService assetService;

    private readonly ICurrentUserContextService currentUserContextService;

    public AssetController(
        IAssetService assetService,
        ICurrentUserContextService currentUserContextService
    )
    {
        this.assetService = assetService;
        this.currentUserContextService = currentUserContextService;
    }

    [HttpPost]
    public async Task<ActionResult<APIResponse<AssetDTO>>>  Create([FromForm] AssetCreateWrapperDTO assetCreateWrapperDTO)
    {
        var asset = await assetService.CreateAsync(
            assetCreateWrapperDTO,
            currentUserContextService.GetUserId()
        );

        return Ok(APIResponseFactory.CreateSuccess(asset));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<APIResponse<bool>>>  Delete(int id)
    {
        await assetService.DeleteAsync(id, currentUserContextService.GetUserId());

        return Ok(APIResponseFactory.CreateSuccess(true));
    }

    [HttpPut]
    public async Task<ActionResult<APIResponse<AssetDTO>>>  Update(AssetUpdateWrapperDTO assetUpdateWrapperDTO)
    {
        var asset = await assetService.UpdateAsync(
            assetUpdateWrapperDTO,
            currentUserContextService.GetUserId()
        );

        return Ok(APIResponseFactory.CreateSuccess(asset));
    }
}