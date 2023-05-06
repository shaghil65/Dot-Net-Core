using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Login login)
        {
            var user = _context.User.Where(x => x.Name == login.Name && x.Password == login.Password).Count();
            if (user > 0)
            {
                var claimsIdentity = new ClaimsIdentity(new[] {new Claim(ClaimTypes.NameIdentifier,login.Name) }, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPriciple = new ClaimsPrincipal(claimsIdentity);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,claimsPriciple);
                HttpContext.Session.SetString("user", login.Name);
                return RedirectToAction("Dashboard", "Home");   
            }
            else
            {
                return View();
            }

        }
        [Authorize]
        public IActionResult Dashboard()
        {
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("DashBoard");
        }
        public IActionResult Privacy()
        {
            return View();
        }

    }
}
