using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace StackAGoal.Models.Identity
{
    /// <summary>
    /// The main Database context which will store all the table data
    /// for the models of the application. 
    /// </summary>
    /// <remarks>
    /// The Generic Integers of the base class  IdentityDbContext<ApplicationUser,IdentityRole<int>,int>
    /// represent how we would like to use an INT for the primary key rather then the default nvarchar.    
    /// </remarks>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,IdentityRole<int>,int>
    {
        public DbSet<Goal> Goals { get; set; }
        public DbSet<Category> Categories  { get; set; }
        public DbSet<Icon> Icons { get; set; }
        public DbSet<Checkpoint> Checkpoints { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
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
