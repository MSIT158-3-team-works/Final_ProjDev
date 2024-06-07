using Microsoft.AspNetCore.Mvc;

namespace projFitConnect.Controllers
{
    public class HealthBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
