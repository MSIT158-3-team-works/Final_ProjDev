using Microsoft.AspNetCore.Mvc;

namespace projFitConnect.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult merchandise(int? id)
        {
            if (id == null||id<=0) { return RedirectToAction("Index"); }
            ViewBag.id = id;
            return View();
        }
    }
}
