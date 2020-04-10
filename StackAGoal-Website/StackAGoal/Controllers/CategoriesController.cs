using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackAGoal.Core.Interfaces;
using StackAGoal.Core.Models;
using StackAGoal.ViewModels;
using System.Linq;

namespace StackAGoal.Controllers
{
    /// <summary>
    /// This controller will manage actions to do with Categories.
    /// </summary>
    [Authorize]
    public class CategoriesController : Controller
    {
        private readonly ICategoriesService _categoriesService;
        private readonly IIconsService _iconsService;

        public CategoriesController(ICategoriesService categoriesService,
            IIconsService iconsService)
        {
            _categoriesService = categoriesService;
            _iconsService = iconsService;
        }

        public IActionResult Index()
        {
            var categories = _categoriesService.GetCategories();
            return View(categories);
        }

        public IActionResult CreateCategory()
        {
            var categoryFormVM = new CategoryFormViewModel()
            {
                Icons = _iconsService.GetIcons().ToList()
            };

            return View("CategoryForm", categoryFormVM);
        }

        public IActionResult UpdateCategory(int id)
        {
            var category = _categoriesService.GetCategoryWithIcons(id);
            var categoryViewModel = new CategoryFormViewModel(category)
            {
                Icons = _iconsService.GetIcons()
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
                categoryVM.Icons = _iconsService.GetIcons();
                return View("CategoryForm", categoryVM);
            }

            if (categoryVM.Id == 0)
            {
                var newCategory = new Category();
                newCategory.IconId = categoryVM.IconId;
                newCategory.Name = categoryVM.Name;

                _categoriesService.AddCategory(newCategory);
                _categoriesService.Save();
            }
            else
            {
                var categoryInDb = _categoriesService.GetCategory(categoryVM.Id.Value);
                categoryInDb.IconId = categoryVM.IconId;
                categoryInDb.Name = categoryVM.Name;

                _categoriesService.Save();
            }

            return RedirectToAction("Index");
        }

        public IActionResult DeleteCategory(int id)
        {
            var category = _categoriesService.GetCategory(id);

            if (category == null)
                BadRequest("Category Not found");

            _categoriesService.RemoveCategory(category);
            _categoriesService.Save();

            return RedirectToAction("Index");
        }
    }
}