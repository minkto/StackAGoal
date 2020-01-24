using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackAGoal.Models;
using StackAGoal.Models.Identity;
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
        private readonly ApplicationDbContext context;

        public GoalsController(ApplicationDbContext db)
        {
            context = db;
        }

        /// <summary>
        /// This will load the main view page of the logged in user.
        /// and display their goals.
        /// </summary>
        /// <returns>A view with the Goals.</returns>
        public IActionResult Index()
        {
            // Use the current User logged in.
            int userID = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var userGoals = context.Goals
                .Include(g => g.Category)
                .Include(u => u.User)
                .Where(g => g.UserId == userID).ToList();

            return View(userGoals);
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
                Categories = context.Categories.ToList()
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
            // If Goal is Valid, Save
            var goal = context.Goals.SingleOrDefault(g => g.Id == id);
            var categories = context.Categories.ToList();
            

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
            var goal = context.Goals.SingleOrDefault(g => g.Id == id);

            if (goal == null)
                return BadRequest("Goal Not Found!");

            context.Goals.Remove(goal);
            context.SaveChanges();

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
                var categories = context.Categories.ToList();
                goalFormViewModel.Categories = categories;

                return View("GoalForm", goalFormViewModel);
            }

            if (goalFormViewModel.Goal.Id == 0)
            {
                goalFormViewModel.Goal.UserId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
                context.Add(goalFormViewModel.Goal);
            }

            else
            {
                var goalInDb = context.Goals.SingleOrDefault(g => g.Id == goalFormViewModel.Goal.Id);

                goalInDb.Title = goalFormViewModel.Goal.Title;
                goalInDb.Description = goalFormViewModel.Goal.Description;
                goalInDb.StartDate = goalFormViewModel.Goal.StartDate;
                goalInDb.CategoryId = goalFormViewModel.Goal.CategoryId;
                goalInDb.UserId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
                goalInDb.IsComplete = goalFormViewModel.Goal.IsComplete;
            }

            context.SaveChanges();

            return RedirectToAction("Index", "Goals");
        }
    }
}