using Microsoft.AspNetCore.Mvc;

namespace projFitConnect.Controllers
{
    public class AdminController : adminRouteController
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
        public IActionResult CoachVerify()
        {
            return View();
        }
        public IActionResult CourseList()
        {
            return View();
        }
        public IActionResult CourseApproval()
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

        //public IActionResult GymCreate()
        //{
        //    return View();
        //}
        public IActionResult GymReviewList()
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
        public IActionResult FieldReview()
        {
            return View();
        }
        public IActionResult FieldReviewList()
        {
            return View();
        }
        public IActionResult GymUpdate()
        {
            return View();
        }
        public IActionResult FieldUpdate()
        {
            return View();
        }
    }
}
