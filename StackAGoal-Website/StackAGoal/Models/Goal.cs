using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StackAGoal.Models
{
    public class Goal
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        public string Description { get; set; }

        [DisplayName("Start Date")]
        public DateTime? StartDate { get; set; }
        
        public Category Category { get; set; }

        public int? CategoryId { get; set; }

    }
}
