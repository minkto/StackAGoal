using Microsoft.AspNetCore.Mvc;
using StackAGoal.Models;
using StackAGoal.Models.Identity;
using System.Linq;

namespace StackAGoal.Controllers
{
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

        /// <summary>
        /// Creates a new Cateogry to set.
        /// </summary>
        /// <returns></returns>
        public IActionResult CreateCategory()
        {
            var category = new Category();
            return View("CategoryForm", category);
        }

        public IActionResult UpdateCategory(int id)
        {
            var category = dbContext.Categories.SingleOrDefault(c => c.Id == id);

            if (category == null)
                NotFound("Category Not Found");

            return View("CategoryForm", category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View("CategoryForm", category);
            }

            if (category.Id == 0)
                dbContext.Add(category);

            dbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}