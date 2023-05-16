using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Data;
using Store.Models;

namespace Store.Controllers
{
    public class CategoryController : Controller
    {

        //we can create obj of context class but its a legacy method(Tightly Coupled) instead we use implementation of 
        //context class because we registered a service in program class
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db ;
        }
        public IActionResult Index()
        {
            var categoryList = _db.Categories.ToList();
            return View(categoryList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost,AutoValidateAntiforgeryToken]
        public IActionResult Create(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Category Name and Display Order can't be same");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(category);
                _db.SaveChanges();
                TempData["insert"] = "Category added successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int id)
        {
            if (id > 0)
            {
                var category = _db.Categories.FirstOrDefault(c => c.Id == id);
                return View(category);
            }
            return View();
        }
        [HttpPost, AutoValidateAntiforgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(category);
                _db.SaveChanges();
                TempData["edit"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int id)
        {
            Category? category = _db.Categories.Find(id);
            if (category != null)
            {
                _db.Categories.Remove(category);
                TempData["delete"] = "Category removed successfully";
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
