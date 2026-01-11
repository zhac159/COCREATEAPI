using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories;

public class SurveyAnswerRepository : ISurveyAnswerRepository
{
    private readonly CoCreateDbContext context;

    public SurveyAnswerRepository(CoCreateDbContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<SurveyAnswer>> CreateListAsync(IEnumerable<SurveyAnswer> surveyAnswers)
    {
        await context.SurveyAnswers.AddRangeAsync(surveyAnswers);
        await context.SaveChangesAsync();

        return surveyAnswers;
    }
}