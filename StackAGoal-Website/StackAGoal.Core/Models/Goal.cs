using StackAGoal.Core.Validation;
using StackAGoal.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StackAGoal.Core.Models
{
    public class Goal
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        public string Description { get; set; }

        [GoalValidStartDate]
        [DisplayName("Start Date")]
        [DataType(DataType.DateTime)]
        public DateTime? StartDate { get; set; }

        [DisplayName("Completed")]
        public bool IsComplete { get; set; }

        [GoalValidDateCompleted]
        [DisplayName("Date Completed")]
        public DateTime? DateCompleted { get; set; }

        public Category Category { get; set; }
        public int? CategoryId { get; set; }

        public ICollection<Checkpoint> Checkpoints { get; set; }

        public int UserId { get; set; }

    }
}
