using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackAGoal.Core.Interfaces;
using StackAGoal.Infrastructure;
using StackAGoal.Infrastructure.Identity;
using StackAGoal.Infrastructure.Repositories;
using StackAGoal.Infrastructure.Services;
using StackAGoal.Services;

namespace StackAGoal
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ApplicationUser, IdentityRole<int>>()
                    .AddEntityFrameworkStores<AppDbContext>()
                    .AddDefaultUI()
                    .AddDefaultTokenProviders();


            services.AddScoped<IGoalsService, GoalsService>();
            services.AddScoped<ICategoriesService, CategoriesService>();
            services.AddScoped<IIconsService, IconsService>();
            services.AddScoped<ICheckpointsService, CheckpointsService>();

            services.AddTransient<IEmailSender, EmailService>(s => new EmailService(Configuration["EmailService:Host"],
                                                                            Configuration.GetValue<int>("EmailService:Port"),
                                                                            Configuration["EmailService:UserName"],
                                                                            Configuration["EmailService:Password"],
                                                                            Configuration.GetValue<bool>("EmailService:EnableSSL"))
            );
            services.AddTransient<EmailTemplateService>();
            services.AddMvc()
                .AddRazorPagesOptions(options=>
            {
                // Make the login page the first page.
                options.Conventions.AddAreaPageRoute("Identity", "/Account/Login", "");          
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            app.UseCookiePolicy();


        }
    }
}
