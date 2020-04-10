using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StackAGoal.Core.Models;
using StackAGoal.Infrastructure.Identity;

namespace StackAGoal.Infrastructure
{
    public class AppDbContext: IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
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
            builder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims", "dbo");
            builder.Entity<IdentityRole<int>>().ToTable("Roles", "dbo");
            builder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims", "dbo");
            builder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins", "dbo");
            builder.Entity<IdentityUserRole<int>>().ToTable("UserRoles", "dbo");
            builder.Entity<ApplicationUser>().ToTable("Users", "dbo");
            builder.Entity<IdentityUserToken<int>>().ToTable("UserTokens", "dbo");

        }

    }
}
