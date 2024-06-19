using Microsoft.AspNetCore.Mvc;
using projFitConnect.Models;
using projFitConnect.ViewModels;
using System.Diagnostics;

namespace projFitConnect.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Session([FromForm] C_user user)
        {
            int ID = 0;
            int R_ID = 0;
            if (user == null)
                return RedirectToAction("Index", "home");
            Console.WriteLine(user.id);
            bool a = int.TryParse(user.id, out ID);
            bool b = int.TryParse(user.role_id, out R_ID);
            if (a && b)
            {
                HttpContext.Session.SetInt32("ID", ID);
                HttpContext.Session.SetInt32("role_ID", R_ID);
            }
            else
                return RedirectToAction("Index", "home");

            if (R_ID == 3)
                return RedirectToAction("Index", "admin");

            return RedirectToAction("", "home");
        }

        public IActionResult Policy()
        {
            //  服務條款 勿刪勿改名
            return View();
        }

        public IActionResult Privacy()
        {
            //  隱私權政策 勿刪勿改名
            return View();
        }
        public IActionResult Service()
        {
            //  退款政策 勿刪勿改名
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
