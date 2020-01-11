using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using StackAGoal.Models;
using StackAGoal.Models.Identity;
using StackAGoal.ViewModels;

namespace StackAGoal.Controllers
{
    public class CheckpointsController : Controller
    {
        protected ApplicationDbContext dbContext;

        public CheckpointsController(ApplicationDbContext context)
        {
            dbContext = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult CreateCheckpoint([FromBody]Checkpoint checkpoint)
        {
            if (ModelState.IsValid)
            {
                dbContext.Add(checkpoint);
                dbContext.SaveChanges();
            }

            return Json(new
            {
                id = checkpoint.Id,
                description = checkpoint.Description
            });
        }

        [HttpPut]
        public JsonResult UpdateCheckpointsByGoal([FromBody]Checkpoint checkpoint)
        {
            int result = 0;
            var checkpointDb = dbContext.Checkpoints.SingleOrDefault(c => c.Id == checkpoint.Id);

            if (ModelState.IsValid)
            {
                checkpointDb.Description = checkpoint.Description;
                checkpointDb.IsComplete = checkpoint.IsComplete;
                dbContext.SaveChanges();
            }

            return Json(new { result = result });
        }

        [HttpGet]
        public JsonResult GetCheckpointsByGoal(int goalId)
        {
            var checkpoints = dbContext.Checkpoints.Where(ch => ch.GoalId == goalId).ToList();
            var chkVM = new CheckpointsModalViewModel()
            {
                GoalId = goalId,
                Checkpoints = checkpoints
            };
            return Json(new { checkpoints = chkVM.Checkpoints });
        }

        [HttpDelete]
        public JsonResult DeleteCheckpoint(int id)
        {
            int deleteResult = 0;
            var checkpoint = dbContext.Checkpoints.SingleOrDefault(c => c.Id == id);
            if (checkpoint != null)
            {
                dbContext.Remove(checkpoint);
                deleteResult = dbContext.SaveChanges();
            }

            return Json(new { result = deleteResult });
        }
    }
}