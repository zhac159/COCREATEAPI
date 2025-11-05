// using Application.Features.Common.Records;
// using FluentValidation;
// using Infrastructure.Persistence;
// using Mediator;

// namespace Application.Features.ProjectFeature.CreateProject;

// public sealed record CreateProjectRequest : ICommand<ProjectRecord> { }

// public sealed class CreateProjectRequestValidator : AbstractValidator<CreateProjectRequest>
// {
//     public CreateProjectRequestValidator()
//     {
//         RuleFor(x => x).NotNull();
//     }
// }

// public sealed class CreateProjectRequestHandler(CoCreateDbContext coCreateDbContext)
//     : ICommandHandler<CreateProjectRequest, CreateProjectResponse>
// {
//     public async ValueTask<CreateProjectResponse> Handle(
//         CreateProjectRequest command,
//         CancellationToken cancellationToken
//     )
//     {
//         // TODO: implement handler logic
//         throw new System.NotImplementedException();
//     }
// }
