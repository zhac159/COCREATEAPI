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
    public async Task<ActionResult<APIResponse<AssetDTO>>>  Create(AssetCreateDTO assetCreateDTO)
    {
        var asset = await assetService.CreateAsync(
            assetCreateDTO
        );

        return Ok(APIResponseFactory.CreateSuccess(asset));
    }

    [HttpPut]
    public async Task<ActionResult<APIResponse<AssetDTO>>>  Update(AssetUpdateDTO assetUpdateDTO)
    {
        var asset = await assetService.UpdateAsync(
            assetUpdateDTO
        );

        return Ok(APIResponseFactory.CreateSuccess(asset));
    }

    // [HttpDelete("{id}")]
    // public async Task<ActionResult<APIResponse<bool>>>  Delete(int id)
    // {
    //     await assetService.DeleteAsync(id, currentUserContextService.GetUserId());

    //     return Ok(APIResponseFactory.CreateSuccess(true));
    // }

}