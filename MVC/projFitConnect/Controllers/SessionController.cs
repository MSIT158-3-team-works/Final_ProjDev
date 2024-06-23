using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace projFitConnect.Controllers
{
    public class SessionController : Controller
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            if (!(HttpContext.Session.Keys.Contains("ID") && HttpContext.Session.Keys.Contains("role_ID")))
                context.Result = new RedirectToRouteResult("Home Page", new { controller = "Home", action = "" });
        }
    }
}
