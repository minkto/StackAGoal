using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StackAGoal.Models
{
    public class Icon
    {
        public int Id { get; set; }

        public string ImageURL { get; set; }

        [StringLength(255)]
        public string Description { get; set; }
    }
}
