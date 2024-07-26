namespace Application.DTOs.SurveryAnswerDTOs;

public class CreateSurveyAnswerListDTO
{
    public required List<CreateSurveyAnswerDTO> SurveyAnswers { get; set; } =
        new List<CreateSurveyAnswerDTO>();
}
