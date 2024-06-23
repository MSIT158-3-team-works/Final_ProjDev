using Microsoft.AspNetCore.Mvc;

namespace projFitConnect.Controllers
{
    public class SchduleController : Controller
    {
        public IActionResult Update(int? id)
        {
            ViewBag.id = HttpContext.Session.GetInt32("ID");
            return View();
        }
        [HttpPost]
        public IActionResult Update()
        {
            //  find where to use

            return RedirectToAction("Profile", "Coach");
        }
    }
}
