using Microsoft.AspNetCore.Identity;

namespace StackAGoal.Infrastructure.Identity
{
    /// <summary>
    /// StackAGoal Identity User Role which uses an Integer as the primary key.
    /// </summary>
    public class AppUserRole : IdentityUserRole<int>
    {
    }
}
