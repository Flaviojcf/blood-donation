using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BloodDonationSystem.API.Filter
{
    public class ValidationFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var messages = context.ModelState.SelectMany(ms => ms.Value.Errors)
                                                 .Select(message => message.ErrorMessage)
                                                 .ToList();

                context.Result = new BadRequestObjectResult(messages);
            }
        }
    }
}
