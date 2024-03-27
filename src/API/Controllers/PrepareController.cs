using API.Factories;
using Application.DTOs.PrepareDTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class PrepareController : COCREATEAPIControllerBase
{
    private readonly IPrepareService prepareService;
    
    public PrepareController(IPrepareService prepareService)
    {
        this.prepareService = prepareService;
    }

    [HttpPost]
    public ActionResult<PrepareUploadResponseDTO> Upload(List<PrepareUploadDTO> prepareUploadDTO)
    {

         var prepareUploads = prepareService.Upload(prepareUploadDTO);

        return Ok(APIResponseFactory.CreateSuccess(prepareUploads));
        
    }
}
