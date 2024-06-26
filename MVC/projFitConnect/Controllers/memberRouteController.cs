using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace projFitConnect.Controllers
{
    public class memberRouteController : SessionController
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            if (!(HttpContext.Session.GetInt32("role_ID") == 1 || HttpContext.Session.GetInt32("role_ID") == 4))
                context.Result = new RedirectToRouteResult("Home Page", new { controller = "Home", action = "" });
        }
    }
}