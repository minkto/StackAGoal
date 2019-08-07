using System;
using System.ComponentModel;

namespace StackAGoal.Models
{
    public class Goal
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        [DisplayName("Start Date")]
        public DateTime? StartDate { get; set; }
    }
}
