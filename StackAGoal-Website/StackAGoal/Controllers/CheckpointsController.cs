using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackAGoal.Core.Interfaces;
using StackAGoal.Core.Models;
using StackAGoal.ViewModels;
using System.Linq;

namespace StackAGoal.Controllers
{

    /// <summary>
    /// This controller is used to manage Checkpoints.
    /// </summary>
    [Authorize]
    public class CheckpointsController : Controller
    {
        private readonly ICheckpointsService _checkpointsService;

        public CheckpointsController(ICheckpointsService checkpointsService)
        {
            _checkpointsService = checkpointsService;
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
                _checkpointsService.AddCheckpoint(checkpoint);
                _checkpointsService.Save();
            }

            return Json(new
            {
                id = checkpoint.Id,
                description = checkpoint.Description
            });
        }

        [HttpPut]
        public JsonResult UpdateCheckpoint([FromBody]Checkpoint checkpoint)
        {
            int updateResult = 0;
            var checkpointDb = _checkpointsService.GetCheckpoint(checkpoint.Id);

            if (ModelState.IsValid)
            {
                checkpointDb.Description = checkpoint.Description;
                checkpointDb.IsComplete = checkpoint.IsComplete;
                updateResult = _checkpointsService.Save();
            }

            return Json(new { result = updateResult });
        }

        [HttpGet]
        public JsonResult GetCheckpointsByGoal(int goalId)
        {
            var checkpoints = _checkpointsService.GetCheckpointsByGoal(goalId).ToList();
            var chkVM = new CheckpointsModalViewModel()
            {
                GoalId = goalId,
                Checkpoints = checkpoints
            };
            return Json(new { checkpoints = chkVM.Checkpoints });
        }

        [HttpDelete]
        public JsonResult DeleteCheckpoint([FromBody]int id)
        {
            int deleteResult = 0;
            var checkpoint = _checkpointsService.GetCheckpoint(id);
            if (checkpoint != null)
            {
                _checkpointsService.RemoveCheckpoint(checkpoint);
                deleteResult = _checkpointsService.Save();
            }

            return Json(new { result = deleteResult });
        }
    }
}