using EPiServer;
using EPiServer.Cms.Shell;
using EPiServer.Cms.UI.AspNetIdentity;
using EPiServer.Scheduler;
using EPiServer.ServiceLocation;
using EPiServer.Web.Routing;
using Microsoft.AspNetCore.Mvc.Razor;

namespace OptimizelyTestProject;

public class Startup
{
    private readonly IWebHostEnvironment _webHostingEnvironment;

    const string cacheMaxAge = "604800";

    public Startup(IWebHostEnvironment webHostingEnvironment)
    {
        _webHostingEnvironment = webHostingEnvironment;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddRazorPages();

        services
            .AddCms()
            .AddHttpContextAccessor()
            .AddCmsAspNetIdentity<ApplicationUser>()
            .AddAdminUserRegistration()
            .AddEmbeddedLocalization<Startup>()
            .Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationFormats.Add("/Views/Pages/{1}/{0}" + RazorViewEngine.ViewExtension);
                options.ViewLocationFormats.Add("/Views/Shared/Components/{1}/{0}" + RazorViewEngine.ViewExtension);
                options.ViewLocationFormats.Add("/Views/Shared/Components/Navigation/{0}" + RazorViewEngine.ViewExtension);
                options.ViewLocationFormats.Add("/CustomTools/Views/{1}/{0}" + RazorViewEngine.ViewExtension);
            });

        /*services.Configure<CookiePolicyOptions>(options => {
            options.CheckConsentNeeded = context => true;
            options.MinimumSameSitePolicy = SameSiteMode.Strict;
        });*/

        if (!_webHostingEnvironment.IsDevelopment())
        {
            //services.AddCmsCloudPlatformSupport(_configuration);
        }
        else
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(_webHostingEnvironment.ContentRootPath, "App_Data"));

            services.Configure<SchedulerOptions>(options => options.Enabled = false);
        }
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (!env.IsDevelopment())
        {
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers.Append(
                         "Cache-Control", $"public, max-age={cacheMaxAge}");
                }
            });
        }
        else
        {
            app.UseStaticFiles();
            app.UseDeveloperExceptionPage();
        }

        //app.UseStatusCodePagesWithReExecute("/error/{0}");
        //app.UseNotFoundHandler();
        //app.UseOptimizelyNotFoundHandler();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseCookiePolicy();
        //app.UseImageVaultHandlers();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapContent();
            endpoints.MapRazorPages();
        });
    }
}
