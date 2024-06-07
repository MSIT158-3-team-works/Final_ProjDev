using Microsoft.AspNetCore.Mvc;

namespace projFitConnect.Controllers
{
    public class MemberController : Controller
    {
        public IActionResult Profile()
        {
            return View();
        }
    }
}
