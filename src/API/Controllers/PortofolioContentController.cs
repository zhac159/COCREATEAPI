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
    public async Task<ActionResult<APIResponse<PortofolioContentDTO>>> Post(
        [FromForm] PortofolioContentCreateWrapperDTO portofolioContentCreateWrapperDTO
    )
    {
        var portofolioContent = await portofolioContentService.CreateAsync(
            portofolioContentCreateWrapperDTO,
            currentUserContextService.GetUserId()
        );
        return Ok(portofolioContent);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await portofolioContentService.DeleteAsync(id, currentUserContextService.GetUserId());

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Update(PortofolioContentUpdateWrapperDTO portofolioContentUpdateWrapperDTO)
    {
        var portofolioContent = await portofolioContentService.UpdateAsync(
            portofolioContentUpdateWrapperDTO,
            currentUserContextService.GetUserId()
        );

        return Ok(portofolioContent);
    }
}
