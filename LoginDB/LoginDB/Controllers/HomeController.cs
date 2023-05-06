using LoginDB.Data;
using LoginDB.Models;
using Microsoft.AspNetCore.Mvc;

namespace LoginDB.Controllers
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
            var user = _context.UserDetails.Where(x => x.Name == login.Name && x.Password == login.Password).Count();
            if (user > 0)
            {
                HttpContext.Session.SetString("user", login.Name);
                return RedirectToAction("DashBoard");
            }
            else
            {
                return View();
            }
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("user"); 
            return RedirectToAction("index");
        }
        public IActionResult Dashboard()
        {
            return View();
        }

    }
}
