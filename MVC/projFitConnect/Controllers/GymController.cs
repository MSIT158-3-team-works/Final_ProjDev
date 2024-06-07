using Microsoft.AspNetCore.Mvc;

namespace projFitConnect.Controllers
{
    public class GymController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
