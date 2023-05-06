using Microsoft.AspNetCore.Mvc;
using MyWebApp.Data;
using MyWebApp.Models;

namespace MyWebApp.Controllers
{
    public class CategoryController : Controller
    {
        private ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> category = _context.Categories;
            return View(category);
        }
        
        //[HttpPost,AutoValidateAntiforgeryToken]
        //public IActionResult Index(string search)
        //{
        //    IEnumerable<Category> cat = Search(search);
            
        //    return View(cat);
        //}

        //public IEnumerable<Category> Search(string search)
        //{
        //    IEnumerable<Category> category = _context.Categories;
        //    category = _context.Categories.Where(x => x.Name.Contains(search)).ToList();
        //    return category;
        //}

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost, AutoValidateAntiforgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Edit(int id)
        {
            if (id > 0)
            {
                var category = _context.Categories.FirstOrDefault(c => c.Id == id);
                return View(category);
            }
            return View();
        }
        [HttpPost, AutoValidateAntiforgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Update(category);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete(int id)
        {
            var category = _context.Categories.Find(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");

        }
    }
}
