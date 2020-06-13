using Microsoft.AspNetCore.Identity;

namespace StackAGoal.Infrastructure.Identity
{
    /// <summary>
    /// StackAGoal Identity Role Claim which uses an Integer as the primary key.
    /// </summary>
    public class AppRoleClaim : IdentityRoleClaim<int>
    {
    }
}
