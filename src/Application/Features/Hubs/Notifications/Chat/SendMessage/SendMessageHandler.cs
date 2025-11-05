using Application.Exceptions;
using Application.Features.Hubs.Notifications.Chat.Common;
using FluentValidation;
using Infrastructure.Interfaces;
using Mediator;
using Microsoft.AspNetCore.SignalR;

namespace Application.Features.Hubs.Notifications.Chat.SendMessage;

public sealed record SendMessageRequest : ICommand<SendMessageResponse>
{
    public required ChatMessage Message { get; set; }
}

public sealed class SendMessageRequestValidator : AbstractValidator<SendMessageRequest>
{
    public SendMessageRequestValidator()
    {
        RuleFor(x => x).NotNull();
    }
}

public sealed class SendMessageRequestHandler(
    IHubContext<NotificationHub> hubContext,
    ICurrentHubUser currentHubUser
) : ICommandHandler<SendMessageRequest, SendMessageResponse>
{
    public async ValueTask<SendMessageResponse> Handle(
        SendMessageRequest command,
        CancellationToken cancellationToken
    )
    {
        var userId = currentHubUser.UserId;

        if (command.Message.SenderId != userId)
        {
            throw new UnauthorizedException("UserId does not match SenderId");
        }

        await hubContext.Clients.All.SendAsync(
            "ReceiveMessage",
            command.Message,
            cancellationToken
        );
        return new SendMessageResponse { Success = true, MessageId = Guid.NewGuid().ToString() };
    }
}
