using Microsoft.AspNetCore.Identity;

namespace StackAGoal.Infrastructure.Identity
{
    /// <summary>
    /// StackAGoal Identity User Token which uses an Integer as the primary key.
    /// </summary>
    public class AppUserToken : IdentityUserToken<int>
    {
    }
}
