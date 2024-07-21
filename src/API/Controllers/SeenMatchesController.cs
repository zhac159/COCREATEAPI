using API.Factories;
using API.Models;
using Application.DTOs.AssetDTOs;
using Application.DTOs.SeenMatchesDTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class SeenMatchesController : COCREATEAPIControllerBase
{
    private readonly ISeenMatchesService seenMatchesService;

    public SeenMatchesController(ISeenMatchesService seenMatchesService)
    {
        this.seenMatchesService = seenMatchesService;
    }

    [HttpPost]
    public async Task<ActionResult<APIResponse<SeenMatchesDTO>>> Create(
        SeenMatchesCreateDTO seenMatchesCreateDTO
    )
    {
        var seenMatches = await seenMatchesService.CreateAsync(seenMatchesCreateDTO);

        return Ok(APIResponseFactory.CreateSuccess(seenMatches));
    }
}
