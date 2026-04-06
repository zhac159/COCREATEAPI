using API.Factories;
using Application.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;

namespace API.Filters;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken
    )
    {
        logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

        var (response, statusCode) = exception switch
        {
            ValidationException ve => (
                APIResponseFactory.CreateError<string>(
                    "validation.error",
                    string.Join("; ", ve.Errors.Select(e => e.ErrorMessage))
                ),
                StatusCodes.Status400BadRequest
            ),
            AppException ae => (
                APIResponseFactory.CreateError<string>(ae.ErrorCode, ae.Message),
                (int)ae.StatusCode
            ),
            BadHttpRequestException bre => (
                APIResponseFactory.CreateError<string>(
                    "json.error",
                    "Invalid JSON in request body."
                ),
                StatusCodes.Status400BadRequest
            ),
            _ => (
                APIResponseFactory.CreateError<string>(
                    "internal.error",
                    "An internal error occurred."
                ),
                StatusCodes.Status500InternalServerError
            ),
        };

        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

        return true;
    }
}
