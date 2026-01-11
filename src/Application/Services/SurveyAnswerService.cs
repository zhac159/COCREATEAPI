using Application.DTOs.SurveryAnswerDTOs;
using Application.Interfaces;
using Application.Extensions;
using Domain.Interfaces;

namespace Application.Services;

public class SurveyAnswerService : ISurveyAnswerService
{
    private readonly ISurveyAnswerRepository surveyAnswerRepository;

    public SurveyAnswerService(ISurveyAnswerRepository surveyAnswerRepository)
    {
        this.surveyAnswerRepository = surveyAnswerRepository;
    }

    public async Task<SurveyAnswerListDTO> CreateSurveryAnswerList(CreateSurveyAnswerListDTO surveyAnswerListDTO)
    {
        var surveyAnswers = surveyAnswerListDTO.SurveyAnswers.Select(surveyAnswer => surveyAnswer.ToEntity()).ToList();

        await surveyAnswerRepository.CreateListAsync(surveyAnswers);

        var surveyAnswerDTOs = surveyAnswers.Select(surveyAnswer => surveyAnswer.ToDTO()).ToList();

        return new SurveyAnswerListDTO
        {
            SurveyAnswers = surveyAnswerDTOs
        };
    }
}