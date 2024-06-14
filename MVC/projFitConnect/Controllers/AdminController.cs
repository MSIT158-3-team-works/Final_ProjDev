using Microsoft.AspNetCore.Mvc;

namespace projFitConnect.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult MemberList()
        {
            return View();
        }
        public IActionResult MemberAuthority()
        {
            return View();
        }
        public IActionResult CoachList()
        {
            return View();
        }
        public IActionResult CoachAuthority()
        {
            return View();
        }
        public IActionResult ProductList()
        {
            return View();
        }
        public IActionResult ProductCreate()
        {
            return View();
        }
        public IActionResult ProductEdit(int? id)
        {
            if (id == null || id <= 0) { return RedirectToAction("ProductList"); }
            ViewBag.id = id;
            return View();
        }
        public IActionResult OrderList()
        {
            return View();
        }

        public IActionResult GymCreate()
        {
            return View();
        }
        public IActionResult ReviewList()
        {
            return View();
        }
        public IActionResult GymReview()
        {
            return View();
        }
        public IActionResult FieldCreate()
        {
            return View();
        }
    }
}
