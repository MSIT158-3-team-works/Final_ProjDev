using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using projFitConnect.ViewModels;

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

        [HttpGet]
        public IActionResult Login() 
        {
            int? ID = HttpContext.Session.GetInt32("ID");
            int? roleId = HttpContext.Session.GetInt32("role_ID");
            if (ID == null)            
                return NotFound();            
            C_user user = new C_user();
            user.id = ID.ToString();
            user.role_id = roleId.ToString(); 
            
            return Ok(user);
        }
    }
}
