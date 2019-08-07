using Microsoft.AspNetCore.Mvc;
using StackAGoal.Data;
using StackAGoal.Models;
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
            var newGoal = new Goal();
            return View("GoalForm", newGoal);
        }

        public IActionResult Edit(int id)
        {
            // If Goal is Valid, Save
            var goal = context.Goals.SingleOrDefault(g => g.Id == id);

            if (goal == null)
                return BadRequest("Goal Not Found!");

            return View("GoalForm", goal);
        }

        public IActionResult Delete(int id)
        {
            // If Goal is Valid, Save
            var goal = context.Goals.SingleOrDefault(g => g.Id == id);

            if (goal == null)
                return BadRequest("Goal Not Found!");

            context.Goals.Remove(goal);
            context.SaveChanges();

            return RedirectToAction("Index");
        }


        public IActionResult Save(Goal goal)
        {
            var goalInDb = context.Goals.SingleOrDefault(g => g.Id == goal.Id);

            if (goalInDb != null)
            {
                goalInDb.Title = goal.Title;
                goalInDb.Description = goal.Description;
                goalInDb.StartDate = goal.StartDate;
            }
            else
            {
                context.Add(goal);
            }

            context.SaveChanges();            
            return RedirectToAction("Index");
        }        
    }
}