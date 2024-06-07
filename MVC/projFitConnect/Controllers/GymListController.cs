using Microsoft.AspNetCore.Mvc;

namespace projFitConnect.Controllers
{
    public class GymListController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
