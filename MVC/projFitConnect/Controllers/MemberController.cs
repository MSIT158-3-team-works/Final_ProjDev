using Microsoft.AspNetCore.Mvc;

namespace projFitConnect.Controllers
{
    public class MemberController : Controller
    {
        public IActionResult Profile()
        {
            return View();
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
    }
}
