using StackAGoal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackAGoal.ViewModels
{
    public class GoalFormViewModel
    {
        public Goal Goal { get; set; }

        public int UserID { get; set; }

        public int? CategoryId { get; set; }

        public List<Category> Categories { get; set; }

    }
}
