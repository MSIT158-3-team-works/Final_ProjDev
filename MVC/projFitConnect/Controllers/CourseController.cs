using Microsoft.AspNetCore.Mvc;

namespace projFitConnect.Controllers
{
    public class CourseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Search(int? id)
        {
            if (id == null || id <= 0) { return RedirectToAction("Index"); }
            ViewBag.classScheduleId = id;
            if (HttpContext.Session.Keys.Contains("ID"))
            {
                ViewBag.ID = HttpContext.Session.GetInt32("ID");
            }
            return View();
        }
    }
}
