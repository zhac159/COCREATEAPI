namespace Application.DTOs.SurveryAnswerDTOs;

using Domain.Entities;

public class CreateSurveyAnswerDTO
{
    public int UserId { get; set; }
    public int SurveyId { get; set; }
    public int QuestionId { get; set; }
    public string? Answer { get; set; }

    public SurveyAnswer ToEntity()
    {
        return new SurveyAnswer
        {
            UserId = UserId,
            SurveyId = SurveyId,
            QuestionId = QuestionId,
            Answer = Answer
        };
    }
}
