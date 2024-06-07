using Microsoft.AspNetCore.Mvc;

namespace projFitConnect.Controllers
{
    public class CourseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Search()
        {
            return View();
        }
    }
}
