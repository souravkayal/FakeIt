using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FakeIt.Web.Filters
{
    public class CustomValidationFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                    .Where(e => e.Value?.Errors.Count > 0)
                    .Select(e => new
                    {
                        Field = e.Key,
                        Errors = e.Value?.Errors.Select(x => x.ErrorMessage).ToArray()
                    });

                var errorResponse = new
                {
                    Title = "Validation Failed",
                    Errors = errors
                };

                context.Result = new BadRequestObjectResult(errorResponse);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Do nothing
        }
    }
}
