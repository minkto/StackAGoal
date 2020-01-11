using StackAGoal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackAGoal.ViewModels
{
    public class CheckpointsModalViewModel
    {
        public List<Checkpoint> Checkpoints { get; set; }
        public int GoalId { get; set; }
    }
}
