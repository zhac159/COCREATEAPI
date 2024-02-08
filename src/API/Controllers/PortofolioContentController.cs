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
        [FromForm] PortofolioContentCreateWrapperDTO portofolioContentCreateWrapperDTO
    )
    {
        var portofolioContent = await portofolioContentService.CreateAsync(
            portofolioContentCreateWrapperDTO,
            currentUserContextService.GetUserId()
        );

        return Ok(APIResponseFactory.CreateSuccess(portofolioContent));
    }
    
    [HttpPut]
    public async Task<ActionResult<APIResponse<PortofolioContentDTO>>> Update(PortofolioContentUpdateWrapperDTO portofolioContentUpdateWrapperDTO)
    {
        var portofolioContent = await portofolioContentService.UpdateAsync(
            portofolioContentUpdateWrapperDTO,
            currentUserContextService.GetUserId()
        );

        return Ok(APIResponseFactory.CreateSuccess(portofolioContent));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<APIResponse<bool>>> Delete(int id)
    {
        await portofolioContentService.DeleteAsync(id, currentUserContextService.GetUserId());

        return Ok(APIResponseFactory.CreateSuccess(true));
    }
}
