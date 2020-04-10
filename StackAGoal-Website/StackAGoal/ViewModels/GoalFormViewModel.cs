using StackAGoal.Core.Models;
using System.Collections.Generic;

namespace StackAGoal.ViewModels
{
    public class GoalFormViewModel
    {
        public Goal Goal { get; set; }

        public int UserID { get; set; }

        public int? CategoryId { get; set; }

        public List<Category> Categories { get; set; }

        public List<Checkpoint> Checkpoints { get; set; }

    }
}
