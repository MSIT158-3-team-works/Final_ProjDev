using Microsoft.AspNetCore.Mvc;

namespace projFitConnect.Controllers
{
    public class TrainerController : Controller
    {
        public IActionResult index()
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
        public IActionResult Search()
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
        public IActionResult Coach(int? id)
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
            if (id == null || id <= 0) { return RedirectToAction("Search"); }
            ViewBag.coachid = id;
            return View();
        }
    }
}
