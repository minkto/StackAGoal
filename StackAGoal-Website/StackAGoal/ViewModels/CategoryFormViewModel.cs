using StackAGoal.Core.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StackAGoal.ViewModels
{
    public class CategoryFormViewModel
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public int? Id { get; set; }
        public int? IconId { get; set; }
        public string ImageURL { get; set; }
        public IEnumerable<Icon> Icons { get; set; }


        public CategoryFormViewModel()
        {
            Id = 0;
        }

        public CategoryFormViewModel(Category category)
        {
            Id = category.Id;
            IconId = category.IconId;
            Name = category.Name;
            if(category.Icon != null)
            {
                ImageURL = category.Icon.ImageURL;
            }            
        }
    }
}
