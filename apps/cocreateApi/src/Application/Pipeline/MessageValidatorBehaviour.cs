using FluentValidation;
using Mediator;

namespace Application.Pipeline;

public sealed class MessageValidatorBehaviour<TMessage, TResponse>(
    IValidator<TMessage>? validator = null
) : IPipelineBehavior<TMessage, TResponse>
    where TMessage : notnull, IMessage
{
    public async ValueTask<TResponse> Handle(
        TMessage message,
        MessageHandlerDelegate<TMessage, TResponse> next,
        CancellationToken cancellationToken
    )
    {
        if (validator is not null)
        {
            var validationResult = await validator.ValidateAsync(message, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errors);
            }
        }

        return await next(message, cancellationToken);
    }
}
