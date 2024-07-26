using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations;

public class SurveyAnswerConfiguration : IEntityTypeConfiguration<SurveyAnswer>
{
    public void Configure(EntityTypeBuilder<SurveyAnswer> builder)
    {   
        builder.ToTable("SurveryAnswers");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.UserId).IsRequired();
        builder.Property(e => e.SurveyId).IsRequired();
        builder.Property(e => e.QuestionId).IsRequired();
        builder.Property(e => e.Answer).HasDefaultValue(null);
    }
}
