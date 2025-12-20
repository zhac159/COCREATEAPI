using Application.Exceptions;
using Application.Features.Common.Records;
using Application.Interfaces;
using FluentValidation;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Persistence;
using Mediator;

namespace Application.Features.ProjectFeature.CreateProject;

public sealed record CreateProjectRequest : ICommand<ProjectRecord>
{
    public required CreateProjectRecord Project { get; init; }
}

public sealed class CreateProjectRequestValidator : AbstractValidator<CreateProjectRequest>
{
    public CreateProjectRequestValidator()
    {
        RuleFor(x => x).NotNull();
    }
}

public sealed class CreateProjectRequestHandler(
    CoCreateDbContext coCreateDbContext,
    ICurrentUser currentUser,
    ICoinsService coinsService
) : ICommandHandler<CreateProjectRequest, ProjectRecord>
{
    public async ValueTask<ProjectRecord> Handle(
        CreateProjectRequest command,
        CancellationToken cancellationToken
    )
    {
        var project = new Project
        {
            Name = command.Project.Name,
            Description = command.Project.Description,
            Date = command.Project.Date,
            ProjectManagerId = currentUser.GetUserId(),
            Address = command.Project.Location.Address,
            Location = command.Project.Location.ToPoint(),
            ProjectMedias =
            [
                .. command.Project.Medias.Select(m => new ProjectMedia
                {
                    Uri = m.Uri,
                    MediaType = m.MediaType,
                    Order = 1,
                }),
            ],
            ProjectRoles =
            [
                .. command.Project.ProjectRoles.Select(pr => new ProjectRole
                {
                    Name = pr.Name,
                    Description = pr.Description,
                    Cost = pr.Cost,
                    SkillType = pr.SkillType,
                    Remote = pr.Remote,
                }),
            ],
        };

        var totalCost = project.GetTotalCost();

        if (!await coinsService.CanAfford(totalCost))
            throw new NotEnoughCoinsException("Not enough coins to create this project.");

        await coinsService.DeductCoins(totalCost);

        coCreateDbContext.Projects.Add(project);
        await coCreateDbContext.SaveChangesAsync(cancellationToken);

        return ProjectRecord.FromProject(project);
    }
}
