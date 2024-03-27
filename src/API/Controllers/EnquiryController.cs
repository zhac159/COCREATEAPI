using API.Factories;
using API.Models;
using Application.DTOs.EnquiryDTOs;
using Application.DTOs.MessageDTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class EnquiryController : COCREATEAPIControllerBase
{
    private readonly IEnquiryService enquiryService;

    public EnquiryController(
        IEnquiryService enquiryService
    )
    {
        this.enquiryService = enquiryService;
    }

    [HttpPost("create")]
    public async Task<ActionResult<APIResponse<EnquiryDTO>>> Create(
       EnquiryCreateDTO enquiryCreateDTO
    )
    {
        var enquiry = await enquiryService.CreateAsync(
            enquiryCreateDTO
        );

        return Ok(APIResponseFactory.CreateSuccess(enquiry));
    }

    [HttpPost("confirm")]
    public async Task<ActionResult<APIResponse<bool>>> Confirm(
        EnquiryConfirmDTO enquiryConfirmDTO
    )
    {
        var result = await enquiryService.ConfirmAsync(
            enquiryConfirmDTO
        );

        return Ok(APIResponseFactory.CreateSuccess(result));
    }

    [HttpPost("send-message")]
    public ActionResult<APIResponse<MessageDTO>> SendMessage(
        MessageCreateDTO enquiryMessageCreateDTO
    )
    {
        // var message = await enquiryService.SendMessageAsync(
        //     enquiryMessageCreateDTO
        // );

        return Ok(APIResponseFactory.CreateSuccess(true));
    }
}