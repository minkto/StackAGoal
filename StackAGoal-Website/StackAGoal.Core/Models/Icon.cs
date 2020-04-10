using System.ComponentModel.DataAnnotations;

namespace StackAGoal.Core.Models
{
    public class Icon
    {
        public int Id { get; set; }

        public string ImageURL { get; set; }

        [StringLength(255)]
        public string Description { get; set; }
    }
}
