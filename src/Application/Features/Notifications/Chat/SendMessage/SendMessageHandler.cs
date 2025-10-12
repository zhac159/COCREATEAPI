using FluentValidation;
using Mediator;
using Microsoft.AspNetCore.SignalR;

namespace Application.Features.Notifications.Chat.SendMessage;

public sealed record SendMessageRequest : ICommand<SendMessageResponse> { }

public sealed class SendMessageRequestValidator : AbstractValidator<SendMessageRequest>
{
    public SendMessageRequestValidator()
    {
        RuleFor(x => x).NotNull();
    }
}

public sealed class SendMessageRequestHandler(IHubContext<NotificationHub> hubContext)
    : ICommandHandler<SendMessageRequest, SendMessageResponse>
{
    public async ValueTask<SendMessageResponse> Handle(
        SendMessageRequest command,
        CancellationToken cancellationToken
    )
    {
        var userId = int.Parse(Context.UserIdentifier ?? throw new Exception("User id is null"));
    }
}
