using API.Factories;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Filters;

public class APIExceptitionFilter : IExceptionFilter
{
    private readonly ILogger<APIExceptitionFilter> logger;

    public APIExceptitionFilter(ILogger<APIExceptitionFilter> logger)
    {
        this.logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        var exception = context.Exception;

        var response = APIResponseFactory.CreateError<string>(exception.Message);

        logger.LogError(exception, exception.Message);

        var statusCode = exception switch
        {
            InvalidModelException => StatusCodes.Status400BadRequest,
            ArgumentException => StatusCodes.Status400BadRequest,
            InvalidPasswordException => StatusCodes.Status401Unauthorized,
            InsufficientFundsException => StatusCodes.Status400BadRequest,
            UnauthorizedAccessException => StatusCodes.Status403Forbidden,
            EntityNotFoundException => StatusCodes.Status404NotFound,
            EntityAlreadyExistsException => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };

        context.Result = new ObjectResult(response) { StatusCode = statusCode };
    }
}
