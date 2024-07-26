namespace Application.DTOs.SurveryAnswerDTOs;

public class SurveyAnswerDTO
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int SurveyId { get; set; }
    public int QuestionId { get; set; }
    public string? Answer { get; set; }
}