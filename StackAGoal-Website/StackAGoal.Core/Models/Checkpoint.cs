using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StackAGoal.Core.Models
{
    public class Checkpoint
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Description { get; set; }

        public bool IsComplete { get; set; }

        public Goal Goal { get; set; }
        public int GoalId { get; set; }
    }
}
