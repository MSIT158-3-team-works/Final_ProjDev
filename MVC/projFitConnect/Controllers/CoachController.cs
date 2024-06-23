using Microsoft.AspNetCore.Mvc;
using projFitConnect.ViewModels;

namespace projFitConnect.Controllers
{
    public class CoachController : coachRouteController
    {
        public IActionResult Profile()
        {
            ViewBag.id = HttpContext.Session.GetInt32("ID");
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
