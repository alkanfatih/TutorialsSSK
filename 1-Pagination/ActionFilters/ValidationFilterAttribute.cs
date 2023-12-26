using _1_Pagination.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace _1_Pagination.ActionFilters
{
    //-IActionFilter -IAsyncActionFilter -ActionFilterAttribute
    public class ValidationFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.RouteData.Values["controller"];
            var action = context.RouteData.Values["action"];
            //DTO
            var param = context.ActionArguments.SingleOrDefault(p => p.Value.ToString().Contains("DTO")).Value;

            if (param == null)
            {
                context.Result = new BadRequestObjectResult($"Object is null " + $"Controller: {controller}" + $" Action {action}");
                return; //400
            }

            if (!context.ModelState.IsValid)
                context.Result = new UnprocessableEntityObjectResult(context.ModelState); //422
        }
    }
}
