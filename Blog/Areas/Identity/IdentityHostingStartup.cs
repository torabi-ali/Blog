using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Blog.Areas.Identity.IdentityHostingStartup))]
namespace Blog.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}