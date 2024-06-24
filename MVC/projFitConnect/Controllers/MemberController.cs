using Microsoft.AspNetCore.Mvc;
using projFitConnect.ViewModels;

namespace projFitConnect.Controllers
{
    public class MemberController : memberRouteController
    {
        public IActionResult Profile()
        {
            C_user member = new C_user
            {
                id = HttpContext.Session.GetInt32("ID").ToString(),
                role_id = HttpContext.Session.GetInt32("role_ID").ToString()
            };

            return View(member);
        }
        public IActionResult Coachregistered()
        {
            int? ID = HttpContext.Session.GetInt32("ID");

            if (ID.HasValue)
            {
                ViewBag.ID = ID.Value;
            }
            else
            {
                ViewBag.ID = null;
            }
            return View();
        }
        public IActionResult Comment()
        {
            int? ID = HttpContext.Session.GetInt32("ID");

            if (ID.HasValue)
            {
                ViewBag.ID = ID.Value;
            }
            else
            {
                ViewBag.ID = null;
            }
            return View();
        }
    }
}
