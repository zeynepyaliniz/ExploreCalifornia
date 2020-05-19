using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ExploreCalifornia
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler("/error.html");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.Use(async (context, next)=> 
            {
                if (context.Request.Path.Value.StartsWith("/Hello")) 
                {
                    await context.Response.WriteAsync("Hello it's ASP.NET Core");
                }
               
                await next();
            });
            app.Use(async (context, next) => 
            {
                if (context.Request.Path.Value.Contains("invalid")) 
                {
                    throw new Exception("Error!!!!");
                }
                await next();
            });
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("Hello ASP.Net Core!");
            //    });
            //});

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync(" Hello !!!!!");
            });

            app.UseStaticFiles();
            

        }
    }
}
