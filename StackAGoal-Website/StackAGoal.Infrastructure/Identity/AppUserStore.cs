using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace StackAGoal.Infrastructure.Identity
{
    /// <summary>
    /// StackAGoal Identity User Store which uses an Integer as the primary key.
    /// </summary>
    public class AppUserStore : UserStore<ApplicationUser,AppRole,AppDbContext,int,AppUserClaim,AppUserRole,AppUserLogin,AppUserToken,AppRoleClaim>
    {
        /// <summary>
        /// Creates a new instance of the StackAGoal implementation of the Identity User Store.
        /// </summary>
        /// <param name="context">Context</param>
        public AppUserStore(AppDbContext context):base(context)
        {

        }
    }
}
