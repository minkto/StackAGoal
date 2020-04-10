using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackAGoal.Core.Models;
using StackAGoal.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace StackAGoal.Controllers.api
{
    [Route("api/[controller]/[action]")]
    [Area("api")]
    [ApiController]
    public class GoalsController : ControllerBase
    {
        private AppDbContext dbContext;

        public GoalsController(AppDbContext db)
        {
            dbContext = db;
        }

        //GET: api/Goals
        [HttpGet]
        public IActionResult GetGoals()
        {
            var goals = dbContext.Goals.Include(g => g.Category).ToList();
            return Ok(goals);
        }
        
        //GET: api/Goals/1
        [HttpGet("{id}")]
        public IActionResult GetGoal(int id)
        {
            var goal = dbContext.Goals.SingleOrDefault(g => g.Id == id);

            if (goal == null)
                return NotFound();

            return Ok(goal);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetGoalsByUserID(int id)
        {
            var goals = dbContext.Goals
                .Include(g => g.Category)
                //.Where(u => u.UserId == id)
                .ToList();

            if (goals == null)
                return this.Content("No goals found.");
            return Ok(goals);
        }


        //POST: api/Goals
        [HttpPost]
        public IActionResult CreateGoal(Goal goal)
        {
            if (ModelState.IsValid)
            {
                dbContext.Add(goal);
                dbContext.SaveChanges();
            }

            return Ok(goal);
        }

        //PUT: api/Goals/2
        [HttpPut("{id}")]
        public IActionResult UpdateGoal(int id, Goal goal)
        {
            var updatedGoal = dbContext.Goals.SingleOrDefault(g => g.Id == id);

            if (updatedGoal == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                updatedGoal.Title = goal.Title;
                updatedGoal.Description = goal.Description;
                updatedGoal.Category = goal.Category;
                updatedGoal.CategoryId = goal.CategoryId;
                updatedGoal.StartDate = goal.StartDate;
            }

            dbContext.Update(updatedGoal);
            dbContext.SaveChanges();

            return Ok();
        }

        //DELETE: api/Goals/1
        [HttpDelete("{id}")]
        public IActionResult DeleteGoal(int id)
        {
            var goal = dbContext.Goals.SingleOrDefault(g => g.Id == id);

            if (goal == null)
            {
                return NotFound();
            }

            dbContext.Remove(goal);
            dbContext.SaveChanges();

            return Ok();
        }     
    }
}