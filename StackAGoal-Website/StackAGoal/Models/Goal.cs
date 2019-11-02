using Microsoft.AspNetCore.Identity;
using StackAGoal.Models.Identity;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [DataType(DataType.DateTime)]
        public DateTime? StartDate { get; set; }
        
        public Category Category { get; set; }
        public int? CategoryId { get; set; }
        
        public virtual ApplicationUser User { get; set; }
        public int? UserId { get; set; }

    }
}
