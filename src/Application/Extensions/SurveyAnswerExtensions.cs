using Application.DTOs.SurveryAnswerDTOs;
using Domain.Entities;

namespace Application.Extensions;

public static class SurveyAnswerExtensions
{
    public static SurveyAnswerDTO ToDTO(this SurveyAnswer surveyAnswer)
    {
        return new SurveyAnswerDTO
        {
            Id = surveyAnswer.Id,
            UserId = surveyAnswer.UserId,
            SurveyId = surveyAnswer.SurveyId,
            QuestionId = surveyAnswer.QuestionId,
            Answer = surveyAnswer.Answer
        };
    }
}
