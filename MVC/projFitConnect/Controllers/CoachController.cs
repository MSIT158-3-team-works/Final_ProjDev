using Microsoft.AspNetCore.Mvc;

namespace projFitConnect.Controllers
{
    public class CoachController : coachRouteController
    {
        public IActionResult Profile()
        {
            return View();
        }
        public IActionResult Search()
        {
            return View();
        }
        public IActionResult CreateCourse()
        {
            ViewBag.id = HttpContext.Session.GetInt32("ID");
            return View();
        }
    }
}
