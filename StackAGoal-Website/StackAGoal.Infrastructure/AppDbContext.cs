using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StackAGoal.Core.Models;
using StackAGoal.Infrastructure.Identity;

namespace StackAGoal.Infrastructure
{
    /// <summary>
    /// This is the Db context for the application. This inherits from Identity Db Context to enable the use
    /// of the Identity Framework and has been altered to use the Integer data type as the primary key.
    /// </summary>
    public class AppDbContext: IdentityDbContext<ApplicationUser,AppRole,int,AppUserClaim,AppUserRole,AppUserLogin,AppRoleClaim,AppUserToken>
    {
        public DbSet<Goal> Goals { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Icon> Icons { get; set; }
        public DbSet<Checkpoint> Checkpoints { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Used to create/edit table names and properties as the entities
        /// represent the model objects.
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Checkpoint>().ToTable("Checkpoints", "dbo");

            // Rename default identity table names.
            builder.Entity<AppRoleClaim>().ToTable("RoleClaims", "dbo");
            builder.Entity<AppRole>().ToTable("Roles", "dbo");
            builder.Entity<AppUserClaim>().ToTable("UserClaims", "dbo");
            builder.Entity<AppUserLogin>().ToTable("UserLogins", "dbo");
            builder.Entity<AppUserRole>().ToTable("UserRoles", "dbo");
            builder.Entity<ApplicationUser>().ToTable("Users", "dbo");
            builder.Entity<AppUserToken>().ToTable("UserTokens", "dbo");

        }

    }
}
