using API.Factories;
using API.Models;
using Application.DTOs.SurveryAnswerDTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class SurveyAnswerController : COCREATEAPIControllerBase
{
    private readonly ISurveyAnswerService surveyAnswerService;

    public SurveyAnswerController(ISurveyAnswerService surveyAnswerService)
    {
        this.surveyAnswerService = surveyAnswerService;
    }

    [HttpPost("create-list")]
    public async Task<ActionResult<APIResponse<SurveyAnswerListDTO>>> CreateList(
        CreateSurveyAnswerListDTO surveyAnswerListDTO
    )
    {
        var surveyAnswerList = await surveyAnswerService.CreateSurveryAnswerList(
            surveyAnswerListDTO
        );

        return Ok(APIResponseFactory.CreateSuccess(surveyAnswerList));
    }
}
