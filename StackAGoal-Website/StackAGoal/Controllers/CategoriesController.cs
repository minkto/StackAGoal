using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackAGoal.Models;
using StackAGoal.Models.Identity;
using StackAGoal.ViewModels;
using System.Linq;

namespace StackAGoal.Controllers
{
    /// <summary>
    /// This controller will manage actions to do with Categories.
    /// </summary>
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public CategoriesController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var categories = dbContext.Categories.ToList();
            return View(categories);
        }

        public IActionResult CreateCategory()
        {
            var categoryFormVM = new CategoryFormViewModel()
            {
                Icons = dbContext.Icons.ToList()
            };

            return View("CategoryForm", categoryFormVM);
        }

        public IActionResult UpdateCategory(int id)
        {
            var category = dbContext.Categories.Include(i => i.Icon).Single(c => c.Id == id);
            var categoryViewModel = new CategoryFormViewModel(category)
            {
                Icons = dbContext.Icons.ToList()
            };

            if (categoryViewModel == null)
                NotFound("Category Not Found");

            return View("CategoryForm", categoryViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveCategory(CategoryFormViewModel categoryVM)
        {
            if (!ModelState.IsValid)
            {
                categoryVM.Icons = dbContext.Icons.ToList();
                return View("CategoryForm", categoryVM);
            }

            if (categoryVM.Id == 0)
            {
                var newCategory = new Category();
                newCategory.IconId = categoryVM.IconId;
                newCategory.Name = categoryVM.Name;
                dbContext.Add(newCategory);
            }
            else
            {
                var categoryInDb = dbContext.Categories.SingleOrDefault(c => c.Id == categoryVM.Id);
                categoryInDb.Name = categoryVM.Name;
                categoryInDb.IconId = categoryVM.IconId;
            }

            dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult DeleteCategory(int id)
        {
            var category = dbContext.Categories.Single(c => c.Id == id);

            if (category == null)
                BadRequest("Category Not found");

            // This will get the goals associated with this Cateogry and set the CategoryID to null.
            dbContext.Goals.Where(g => g.CategoryId == category.Id).Load();
            dbContext.Remove(category);
            dbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}