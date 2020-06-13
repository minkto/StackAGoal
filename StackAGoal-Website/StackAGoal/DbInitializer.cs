using Microsoft.AspNetCore.Identity;
using StackAGoal.Infrastructure;
using StackAGoal.Infrastructure.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StackAGoal
{
    /// <summary>
    /// This allows the seeding of entities needed by the application. It can be done for entities
    /// which may not be straightfoward to seed like an Identity User.
    /// </summary>
    public static class DbInitializer
    {
        /// <summary>
        /// This will seed the Identity Users/Roles and other required pre set entities.
        /// </summary>
        /// <param name="dbContext">The Database context</param>
        /// <param name="userManager">User Manager to manage users.</param>
        /// <param name="roleManager">Role Manager to manage roles.</param>
        public static void Seed(AppDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<AppRole> roleManager) 
        {
            // Seeding Roles
            SeedRole(roleManager, "Admin").Wait();

            // PA: 13-06-2020 This section contatins the logic, if needed to seed specific users.
            // List<string> roles = roleManager.Roles.Select(x => x.Name).ToList();
            //var adminUser = new ApplicationUser()
            //{
            //    UserName           = string.Empty,
            //    NormalizedUserName = string.Empty,
            //    Email              = string.Empty,
            //    NormalizedEmail    = string.Empty,
            //    EmailConfirmed     = true,
            //    SecurityStamp      = Guid.NewGuid().ToString()
            //};

            //if (!dbContext.Users.Any(u => u.UserName == adminUser.UserName))
            //{
            //    var password = new PasswordHasher<ApplicationUser>();
            //    var hashed = password.HashPassword(adminUser, "Password1@");
            //    adminUser.PasswordHash = hashed;

            //    var userStore = new AppUserStore(dbContext);
            //    userStore.CreateAsync(adminUser).Wait();
            //}

            //SeedRolesToUser(userManager, adminUser.Email, roles).Wait();
            dbContext.SaveChangesAsync().Wait();
        }

        public static async Task<IdentityResult> SeedRole(RoleManager<AppRole> roleManager, string roleName)
        {
            IdentityResult res = null;
            if (!roleManager.RoleExistsAsync(roleName).Result)
            {
                AppRole newRole = new AppRole(roleName);
                res = await roleManager.CreateAsync(newRole);
            }
            return res;
        }

        public static async Task<IdentityResult> SeedRolesToUser(UserManager<ApplicationUser> userManager, string email, List<string> roles)
        {
            ApplicationUser user = await userManager.FindByEmailAsync(email);
            var result = await userManager.AddToRolesAsync(user, roles);

            return result;
        }
    }
}
