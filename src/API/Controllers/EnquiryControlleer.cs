using API.Factories;
using API.Models;
using Application.DTOs.EnquiryDTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class EnquiryController : COCREATEAPIControllerBase
{
    private readonly IEnquiryService enquiryService;
    private readonly ICurrentUserContextService currentUserContextService;

    public EnquiryController(
        IEnquiryService enquiryService,
        ICurrentUserContextService currentUserContextService
    )
    {
        this.enquiryService = enquiryService;
        this.currentUserContextService = currentUserContextService;
    }

    [HttpPost("create")]
    public async Task<ActionResult<APIResponse<EnquiryDTO>>> Create(
       EnquiryCreateDTO enquiryCreateDTO
    )
    {
        var enquiry = await enquiryService.CreateAsync(
            enquiryCreateDTO,
            currentUserContextService.GetUserId()
        );

        return Ok(APIResponseFactory.CreateSuccess(enquiry));
    }

    [HttpPost("confirm")]
    public async Task<ActionResult<APIResponse<bool>>> Confirm(
        EnquiryConfirmDTO enquiryConfirmDTO
    )
    {
        var result = await enquiryService.ConfirmAsync(
            enquiryConfirmDTO,
            currentUserContextService.GetUserId()
        );

        return Ok(APIResponseFactory.CreateSuccess(result));
    }
}