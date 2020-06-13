using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StackAGoal.Infrastructure;
using StackAGoal.Infrastructure.Identity;
using System;

namespace StackAGoal
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            using (host.Services.CreateScope()) 
            {
                var services = host.Services;
                try
                {
                    var roleManager = services.GetService<RoleManager<AppRole>>();
                    var userManager = services.GetService<UserManager<ApplicationUser>>();
                    var dbContext = services.GetService<AppDbContext>();

                    // This will ensure a schema exists to proceed with migrations. 
                    dbContext.Database.Migrate();

                    // After migrations begin necessary seeding.
                    DbInitializer.Seed(dbContext, userManager, roleManager);
                }

                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error has occurred while migrating or initializing the database.");
                }
            }    
            
           host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
