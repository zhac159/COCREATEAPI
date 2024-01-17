using API.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Filters;

public class APIExceptitionFilter: IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var exception = context.Exception;

        var response = APIResponseFactory.CreateError<string>(exception.Message);

        var statusCode = exception switch
        {
            ArgumentException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };

        context.Result = new ObjectResult(response)
        {
            StatusCode = statusCode
        };
    }
}
