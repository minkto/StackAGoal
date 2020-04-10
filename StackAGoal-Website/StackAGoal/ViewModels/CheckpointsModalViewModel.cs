using StackAGoal.Core.Models;
using System.Collections.Generic;

namespace StackAGoal.ViewModels
{
    public class CheckpointsModalViewModel
    {
        public List<Checkpoint> Checkpoints { get; set; }
        public int GoalId { get; set; }
    }
}
