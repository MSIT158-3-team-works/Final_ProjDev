using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using projFitConnect.Models;
using projFitConnect.ViewModels;
using System.Diagnostics;
using System.Net.Http.Headers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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

        [HttpPost]
        public IActionResult Login(int id, int r_id)
        {
            C_user user = new C_user
            {
                id = id.ToString(),
                role_id = r_id.ToString(),
            };
            Console.WriteLine(user.id);
            Console.WriteLine(user.role_id);

            HttpContext.Session.SetInt32("ID", id);
            HttpContext.Session.SetInt32("role_ID", r_id);

            return RedirectToAction("", "home", null);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("", "home");
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

        //public async Task googleLogin()
        //{
        //    await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties
        //    {
        //        RedirectUri = Url.Action("GoogleResponse")
        //    });
        //}
        public async Task<IActionResult> googleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (!result.Succeeded)
            {
                return RedirectToAction("", "home");
            }

            var claims = result.Principal.Identities.FirstOrDefault().Claims.Select(claim => new C_googleLogin
            {
                Issuer = claim.Issuer,
                OriginalIssuer = claim.OriginalIssuer,
                Type = claim.Type,
                Value = claim.Value
            });
            C_googleLogin g_user = claims.FirstOrDefault();

            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await httpClient.GetAsync("https://www.googleapis.com/oauth2/v3/userinfo");
            if (response.IsSuccessStatusCode)
            {
                var userInfo = await response.Content.ReadFromJsonAsync<C_googleLoginProperty>();
                ViewBag.PhotoUrl = userInfo?.photoUrl;
            }

            return RedirectToAction("policy", "home");
        }

        [HttpPost("signout")]
        public async Task<IActionResult> googleSignout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Issuer")))
            //    HttpContext.Session.Clear();
            return RedirectToAction("", "home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
