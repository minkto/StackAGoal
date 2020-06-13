using Microsoft.AspNetCore.Identity;

namespace StackAGoal.Infrastructure.Identity
{
    /// <summary>
    /// StackAGoal Identity Role which uses an Integer as the primary key.
    /// </summary>
    public class AppRole : IdentityRole<int>
    {
        /// <summary>
        /// Creates an instance of the StackAGoal implementation of the Identity Role
        /// </summary>
        public AppRole(): base() { }

        /// <summary>
        /// Creates an instance of the StackAGoal implementation of the Identity Role
        /// with a defined role.
        /// </summary>
        /// <param name="roleName">Name of Role</param>
        public AppRole(string roleName) : base(roleName) { }
    }
}
