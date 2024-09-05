namespace DotnetTemplate.Web
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Serilog;

    public class Startup
    {
        private readonly IHostEnvironment env;

        public Startup(IHostEnvironment env)
        {
            this.env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var mvcBuilder = services.AddControllersWithViews();
            if (env.IsDevelopment())
            {
                mvcBuilder.AddRazorRuntimeCompilation();
            }
        }

        public void Configure(IApplicationBuilder app)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }

            app.UseStaticFiles();
            app.UseSerilogRequestLogging();
            app.UseRouting();
            app.UseEndpoints(ConfigureEndPointsAsync);
        }

        private static void ConfigureEndPointsAsync(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapControllerRoute("default", "{controller=Home}/{action=FirstPage}");
        }
    }
}
