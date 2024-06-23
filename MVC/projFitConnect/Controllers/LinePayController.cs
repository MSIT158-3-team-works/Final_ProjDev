using Microsoft.AspNetCore.Mvc;
using projFitConnect.ViewModels;

namespace projFitConnect.Controllers
{
    public class LinePayController : Controller
    {
        public IActionResult shoppingCart()
        {
            if (!HttpContext.Session.Keys.Contains("ID"))
            {
                return RedirectToAction("", "home");
            }

            ViewBag.memberId = HttpContext.Session.GetInt32("ID");
            return View();
        }
        public IActionResult CourseShoppingCart()
        {
            if (!HttpContext.Session.Keys.Contains("ID"))
            {
                return RedirectToAction("", "home");
            }

            ViewBag.coachId = HttpContext.Session.GetInt32("ID");
            return View();
        }
        [HttpPost]
        public IActionResult Pay(PayVM payVM)
        {
            return View(payVM);
        }
        [HttpPost]
        public IActionResult PayforCourse([FromForm] PayforCourseVM payforCourseVM)
        {
            return View(payforCourseVM);
        }
        public IActionResult Confirm()
        {
            return View();
        }

    }
}
