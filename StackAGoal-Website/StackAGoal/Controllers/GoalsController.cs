using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackAGoal.Data;
using StackAGoal.Models;
using StackAGoal.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StackAGoal.Controllers
{
    /// <summary>
    /// This controller manages the requests associated with 
    /// managing Goals.
    /// </summary>
    public class GoalsController : Controller
    {
        private readonly ApplicationDbContext context;

        public GoalsController(ApplicationDbContext db)
        {
            context = db;
        }

        public IActionResult Index()
        {
            var goals = context.Goals.ToList();
            return View(goals);
        }

        public IActionResult New()
        {
            // If Goal is Valid, Save
            var newGoal = new GoalFormViewModel()
            {
                Goal = new Goal(),
                Categories = context.Categories.ToList()
            };
            return View("GoalForm", newGoal);
        }

        public IActionResult Edit(int id)
        {
            // If Goal is Valid, Save
            var goal = context.Goals.SingleOrDefault(g => g.Id == id);
            var categories = context.Categories.ToList();

            var goalFormViewModel = new GoalFormViewModel()
            {
                Goal = goal,
                Categories = categories
            };

            if (goal == null)
                return BadRequest("Goal Not Found!");

            return View("GoalForm", goalFormViewModel);
        }

        public IActionResult Delete(int id)
        {
            var goal = context.Goals.SingleOrDefault(g => g.Id == id);

            if (goal == null)
                return BadRequest("Goal Not Found!");

            context.Goals.Remove(goal);
            context.SaveChanges();

            return RedirectToAction("Index");
        }


        public IActionResult Save(GoalFormViewModel goalFormViewModel)
        {

            if (!ModelState.IsValid)
            {                
                return View("GoalForm", goalFormViewModel);
            }

            var goalInDb = context.Goals.SingleOrDefault(g => g.Id == goalFormViewModel.Goal.Id);

            if (goalInDb != null)
            {
                goalInDb.Title = goalFormViewModel.Goal.Title;
                goalInDb.Description = goalFormViewModel.Goal.Description;
                goalInDb.StartDate = goalFormViewModel.Goal.StartDate;

            }
            else
            {
                context.Add(goalFormViewModel.Goal);
            }

            context.SaveChanges();
            return RedirectToAction("Index");
        }        
    }
}