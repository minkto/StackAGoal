using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace StackAGoal.Models.Identity
{
    /// <summary>
    /// A customisable IdentityUser Object. Custom properties can be added here. 
    /// </summary>
    public class ApplicationUser:IdentityUser<int>
    {
        public virtual ICollection<Goal> Goals { get; set; }
    }
}
