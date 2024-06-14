using Microsoft.AspNetCore.Mvc;

namespace projFitConnect.Controllers
{
    public class TrainerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Search()
        {
            return View();
        }
        public IActionResult Coach(int? id)
        {
            if (id == null || id <= 0) { return RedirectToAction("Search"); }
            ViewBag.id = id;
            return View();
        }
    }
}
