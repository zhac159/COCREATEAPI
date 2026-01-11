using Application.DTOs.SurveryAnswerDTOs;

namespace Application.Interfaces;

public interface ISurveyAnswerService
{
    Task<SurveyAnswerListDTO> CreateSurveryAnswerList(CreateSurveyAnswerListDTO surveyAnswerListDTO);
}