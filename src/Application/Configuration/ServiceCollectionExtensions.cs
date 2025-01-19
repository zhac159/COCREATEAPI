using Application.Interfaces;
using Application.Services;

namespace Application.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAssetService, AssetService>();
        services.AddScoped<IPortofolioContentService, PortofolioContentService>();
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<IProjectRoleService, ProjectRoleService>();
        services.AddScoped<IEnquiryService, EnquiryService>();
        services.AddScoped<IPrepareService, PrepareService>();
        services.AddScoped<IExperienceService, ExperienceService>();
        services.AddScoped<IReviewService, ReviewService>();
        services.AddScoped<IAssetOfferService, AssetOfferService>();
        services.AddScoped<IVoucherCodeService, VoucherCodeService>();
        services.AddScoped<ISeenMatchesService, SeenMatchesService>();
        services.AddScoped<ISurveyAnswerService, SurveyAnswerService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IChatService, ChatService>();

        return services;
    }
}
