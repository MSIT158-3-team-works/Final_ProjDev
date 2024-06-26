using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace projFitConnect.Controllers
{
    public class coachRouteController : SessionController
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            if (HttpContext.Session.GetInt32("role_ID") != 2)
                context.Result = new RedirectToRouteResult("Home Page", new { controller = "Home", action = "" });
        }
    }
}