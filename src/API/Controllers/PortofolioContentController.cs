using API.Factories;
using API.Models;
using Application.DTOs.PortofolioContentDTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class PortofolioContentController : COCREATEAPIControllerBase
{
    private readonly IPortofolioContentService portofolioContentService;
    private readonly ICurrentUserContextService currentUserContextService;

    public PortofolioContentController(
        IPortofolioContentService portofolioContentService,
        ICurrentUserContextService currentUserContextService
    )
    {
        this.portofolioContentService = portofolioContentService;
        this.currentUserContextService = currentUserContextService;
    }

    [HttpPost]
    public async Task<ActionResult<APIResponse<PortofolioContentDTO>>> Create(
        PortofolioContentCreateDTO portofolioContentCreateDTO
    )
    {
        var portofolioContent = await portofolioContentService.CreateAsync(
            portofolioContentCreateDTO
        );

        return Ok(APIResponseFactory.CreateSuccess(portofolioContent));
    }

    [HttpPut]
    public async Task<ActionResult<APIResponse<PortofolioContentDTO>>> Update(
        PortofolioContentUpdateDTO portofolioContentUpdateDTO
    )
    {
        var portofolioContent = await portofolioContentService.UpdateAsync(
            portofolioContentUpdateDTO
        );

        return Ok(APIResponseFactory.CreateSuccess(portofolioContent));
    }

    [HttpPut("group")]
    public async Task<ActionResult<APIResponse<PortofolioContentDTO>>> UpdateGroup(
        PortofolioContentGroupUpdateDTO portofolioContentGroupUpdateDTO
    )
    {
        var portofolioContent = await portofolioContentService.UpdateGroupAsync(
            portofolioContentGroupUpdateDTO
        );

        return Ok(APIResponseFactory.CreateSuccess(portofolioContent));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<APIResponse<bool>>> Delete(int id)
    {
        var result = await portofolioContentService.DeleteAsync(id);

        return Ok(APIResponseFactory.CreateSuccess(result));
    }
}
