using Microsoft.AspNetCore.Identity;

namespace StackAGoal.Infrastructure.Identity
{
    /// <summary>
    /// A customisable IdentityUser Object. Custom properties can be added here. 
    /// </summary>
    public class ApplicationUser:IdentityUser<int>
    {
    }
}
