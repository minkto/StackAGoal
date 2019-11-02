using StackAGoal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackAGoal.ViewModels
{
    public class CategoryFormViewModel
    {
        public Category Category { get; set; }
        public Icon Icon { get; set; }
        public int IconId { get; set; }
        public IEnumerable<Icon> Icons { get; set; }
    }
}
