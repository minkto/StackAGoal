using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(StackAGoal.Areas.Identity.IdentityHostingStartup))]
namespace StackAGoal.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}