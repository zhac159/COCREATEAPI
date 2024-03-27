using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Filters;

public class ModelValidationFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .Select(x => new { x.Key, x.Value.Errors })
                .ToArray();

            foreach (var error in errors)
            {
                foreach (var subError in error.Errors)
                {
                    // Log the error detail
                    Console.WriteLine($"Property: {error.Key} Error: {subError.ErrorMessage}");
                }
            }

            throw new InvalidModelException();
        }
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}
