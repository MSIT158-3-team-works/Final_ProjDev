using Microsoft.AspNetCore.Mvc;

namespace projFitConnect.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            int? ID = HttpContext.Session.GetInt32("ID");

            if (ID.HasValue)
            {
                ViewBag.ID = ID.Value;
            }
            else
            {
                ViewBag.ID = null;
            }
            return View();
        }
        
        public IActionResult merchandise(int? id)
        {
            int? ID = HttpContext.Session.GetInt32("ID");

            if (ID.HasValue)
            {
                ViewBag.ID = ID.Value;
            }
            else
            {
                ViewBag.ID = null;
            }
            if (id == null||id<=0) { return RedirectToAction("Index"); }
            ViewBag.productid = id;
            return View();
        }
    }
}
