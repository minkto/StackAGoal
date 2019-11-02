using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StackAGoal.Models;
using StackAGoal.Models.Identity;

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
        }
    }
}
