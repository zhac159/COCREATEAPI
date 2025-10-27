// using Application.Exceptions;
// using Application.Features.UserFeature.Common;
// using Application.Interfaces;
// using FluentValidation;
// using Infrastructure.Persistence;
// using Mediator;
// using NetTopologySuite.Geometries;

// namespace Application.Features.UserFeature.PutProfileDetails;

// public sealed record UpdateProfileDetailsRequest : ICommand<ProfileDetails>
// {
//     public required UpdateProfileDetails ProfileDetails { get; set; }
// }

// public sealed class UpdateProfileDetailsRequestValidator
//     : AbstractValidator<UpdateProfileDetailsRequest>
// {
//     public UpdateProfileDetailsRequestValidator()
//     {
//         RuleFor(x => x).NotNull();
//     }
// }

// public sealed class UpdateProfileDetailsRequestHandler(
//     CoCreateDbContext coCreateDbContext,
//     ICurrentUser currentUser
// ) : ICommandHandler<UpdateProfileDetailsRequest, ProfileDetails>
// {
//     public async ValueTask<ProfileDetails> Handle(
//         UpdateProfileDetailsRequest command,
//         CancellationToken cancellationToken
//     )
//     {
//         var user =
//             await coCreateDbContext.Users.FindAsync([currentUser.GetUserId()], cancellationToken)
//             ?? throw new UserNotFoundException("User not found.");

//         user.Username = command.ProfileDetails.Username;
//         user.Email = command.ProfileDetails.Email;
//         user.AboutYou = command.ProfileDetails.AboutYou;

//         user.Location =
//             command.ProfileDetails.Location != null
//                 ? new Point(
//                     command.ProfileDetails.Location.Longitude,
//                     command.ProfileDetails.Location.Latitude
//                 )
//                 {
//                     SRID = 4326,
//                 }
//                 : null;

//         user.Skills = command
//             .ProfileDetails.Skills.Select(s => new Skill
//             {
//                 Name = s.Name,
//                 Proficiency = s.Proficiency,
//             })
//             .ToList();
//     }
// }
