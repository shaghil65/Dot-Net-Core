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
        public async Task<IActionResult> Index()
        {
            return View(await _db.Categories.ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost,AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Category Name and Display Order can't be same");
            }
            if (ModelState.IsValid)
            {
                await _db.Categories.AddAsync(category);
                await _db.SaveChangesAsync();
                TempData["insert"] = "Category added successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id > 0)
            {
                var category = await _db.Categories.FirstOrDefaultAsync(c => c.Id == id);
                if (category != null)
                {
                    return View(category);
                }
            }
            return View();
        }

        [HttpPost, AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(category);
                await _db.SaveChangesAsync();
                TempData["edit"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> Delete(int? id)
        {
            Category? category = await _db.Categories.FindAsync(id);
            if (category != null)
            {
                _db.Categories.Remove(category);
                TempData["delete"] = "Category removed successfully";
                await _db.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

    }
}
