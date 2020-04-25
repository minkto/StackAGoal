using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackAGoal.Core.Interfaces;
using StackAGoal.Core.Models;
using StackAGoal.ViewModels;
using System;
using System.Linq;
using System.Security.Claims;

namespace StackAGoal.Controllers
{
    /// <summary>
    /// This controller manages the requests associated with 
    /// managing Goals.
    /// </summary>
    [Authorize]    
    public class GoalsController : Controller
    {
        private readonly ICategoriesService _categoriesService;
        private readonly IGoalsService _goalsService;

        public GoalsController(ICategoriesService categoriesService, 
            IGoalsService goalsService)
        {
            _categoriesService = categoriesService;
            _goalsService = goalsService;
        }

        /// <summary>
        /// This will load the main view page of the logged in user.
        /// and display their goals.
        /// </summary>
        /// <returns>A view with the Goals.</returns>
        public IActionResult Index()
        {
            //// Use the current User logged in.
            int userID = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var goals = _goalsService.GetGoalsByUser(userID);

            return View(goals);
        }

        /// <summary>
        /// This action will create a new goal.
        /// </summary>
        /// <returns>A view to create goals</returns>
        public IActionResult CreateGoal()
        {
            var newGoal = new GoalFormViewModel()
            {
                Goal = new Goal(),
                Categories = _categoriesService.GetCategories().ToList()
            };
            return View("GoalForm", newGoal);
        }
            
        /// <summary>
        /// This action will help to bring a view to
        /// update the selected goal.
        /// </summary>
        /// <param name="id">Goal ID</param>
        /// <returns>A view to Update goals.</returns>
        public IActionResult UpdateGoal(int id)
        {
            var goal = _goalsService.GetGoal(id);
            var categories = _categoriesService.GetCategories().ToList();            

            var goalFormViewModel = new GoalFormViewModel()
            {
                Goal = goal,
                CategoryId = goal.CategoryId,
                Categories = categories
            };

            if (goal == null)
                return BadRequest("Goal Not Found!");

            return View("GoalForm", goalFormViewModel);
        }

        /// <summary>
        /// This will delete a goal selected.
        /// </summary>
        /// <param name="id">Goal ID</param>
        /// <returns>A </returns>
        public IActionResult DeleteGoal(int id)
        {
            var goal = _goalsService.GetGoal(id);

            if (goal == null)
                return BadRequest("Goal Not Found!");

            _goalsService.RemoveGoal(goal);
            _goalsService.Save();

            return RedirectToAction("Index");
        }

        /// <summary>
        /// This will save the goal. The purpose of the anti-forgery token is due
        /// to avoiding cross-site scripting, attacks etc.
        /// </summary>
        /// <param name="goalFormViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public IActionResult Save(GoalFormViewModel goalFormViewModel)
        {

            if (!ModelState.IsValid)
            {
                var categories = _categoriesService.GetCategories().ToList();
                goalFormViewModel.Categories = categories;

                return View("GoalForm", goalFormViewModel);
            }

            if (goalFormViewModel.Goal.Id == 0)
            {
                goalFormViewModel.Goal.UserId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
                _goalsService.AddNewGoal(goalFormViewModel.Goal);
                _goalsService.Save();
            }
            else
            {
                var goalInDb = _goalsService.GetGoal(goalFormViewModel.Goal.Id);
                goalInDb.Title = goalFormViewModel.Goal.Title;
                goalInDb.Description = goalFormViewModel.Goal.Description;
                goalInDb.StartDate = goalFormViewModel.Goal.StartDate;
                goalInDb.CategoryId = goalFormViewModel.Goal.CategoryId;
                goalInDb.UserId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
                goalInDb.IsComplete = _goalsService.IsGoalCompleted(goalFormViewModel.Goal.IsComplete, goalFormViewModel.Goal.DateCompleted);
                goalInDb.DateCompleted = goalFormViewModel.Goal.DateCompleted;

                _goalsService.Save();
            }

            return RedirectToAction("Index", "Goals");
        }
    }
}