using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace projFitConnect.Controllers
{
    public class adminRouteController : SessionController
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            if (HttpContext.Session.GetInt32("role_ID") != 3)
            {
                context.Result = new RedirectToRouteResult("Redirect", new { controller = "home", action = "" });
            }
            //context.Result = new RedirectToRouteResult("Redirect", new { controller = "jumppages", action = "Redirect3" });
        }
    }
}
