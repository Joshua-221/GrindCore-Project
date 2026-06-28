using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GrindCore.Web.Filters;

public class ApiAuthFilter : IAsyncActionFilter
{
    private const string SessionKey = "IsAuthenticated";

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (context.HttpContext.Session.GetString(SessionKey) != "true")
        {
            context.Result = new UnauthorizedObjectResult(new { message = "Authentication required." });
            return;
        }

        await next();
    }
}
