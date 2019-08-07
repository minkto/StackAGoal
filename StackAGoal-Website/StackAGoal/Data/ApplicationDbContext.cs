using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StackAGoal.Models;

namespace StackAGoal.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Goal> Goals { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {


        }
    }
}
