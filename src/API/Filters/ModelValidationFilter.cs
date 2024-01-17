using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Filters;

public class ModelValidationFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            throw new InvalidModelException();
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }

}
