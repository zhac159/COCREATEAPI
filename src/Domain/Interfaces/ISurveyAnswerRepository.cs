using Domain.Entities;

namespace Domain.Interfaces;

public interface ISurveyAnswerRepository
{
    Task<IEnumerable<SurveyAnswer>> CreateListAsync(IEnumerable<SurveyAnswer> surveyAnswers);
}