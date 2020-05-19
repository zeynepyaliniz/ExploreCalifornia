using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ExploreCalifornia
{
    public class Startup
    {
        private readonly IConfiguration configuration;
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;



        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddRazorPages();
            services.AddControllersWithViews();

            services.AddTransient<FeatureToggles>(x => new FeatureToggles
            {
                DeveloperExceptions = configuration.GetValue<bool>("FeatureToggles:DeveloperExceptions")
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            FeatureToggles featureToggles)
        {
            app.UseExceptionHandler("/error.html");
            //configuration.GetValue<bool>("FeatureToggles:DeveloperExceptions")

            //if (featureToggles.DeveloperExceptions)
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            if (env.IsDevelopment()) 
            {
                app.UseDeveloperExceptionPage();
            }

            app.Use(async (context, next) =>
            {
                if (context.Request.Path.Value.Contains("invalid"))
                    throw new Exception("ERROR!");

                await next();
            });


           // app.UseFileServer();
            app.UseStaticFiles();
           // app.UseMvcWithDefaultRoute();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    name: "areaRoute",
                    pattern: "{area:exists}/{controller}/{action}",
                    defaults: new { action = "Index" });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" });

                endpoints.MapControllerRoute(
                    name: "api",
                    pattern: "{controller}/{id?}");
            });

            ////configuration["EnableDeveloperExceptions"] == "True"; // custom configuration and should  be added to project properties.
            //app.UseExceptionHandler("/error.html");

            //if (configuration.GetValue<bool>("FeatureToggle:EnableDeveloperExceptions")) // bool converting 
            //{
            //    app.UseDeveloperExceptionPage();

            //}

            //app.UseRouting();
            ////app.Use(async (context, next)=> 
            ////{
            ////    if (context.Request.Path.Value.StartsWith("/Hello")) 
            ////    {
            ////        await context.Response.WriteAsync("Hello it's ASP.NET Core");
            ////    }

            ////    await next();
            ////});
            //app.Use(async (context, next) =>
            //{
            //    if (context.Request.Path.Value.Contains("invalid"))                
            //        throw new Exception("Error!!!!");

            //    await next();
            //});
            ////app.UseEndpoints(endpoints =>
            ////{
            ////    endpoints.MapGet("/", async context =>
            ////    {
            ////        await context.Response.WriteAsync("Hello ASP.Net Core!");
            ////    });
            ////});

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync(" Hello !!!!!");
            //});

            //app.UseStaticFiles();


        }
    }
}
