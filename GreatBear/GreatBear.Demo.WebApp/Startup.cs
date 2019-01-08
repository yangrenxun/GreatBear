using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GreatBear.Core;
using GreatBear.Autofac;
using GreatBear.AutoMapper;
using GreatBear.EntityFramework;
using Microsoft.EntityFrameworkCore;
using GreatBear.Demo.EFCore;
using GreatBear.Demo.Application;
//using GreatBear.Dapper;

namespace GreatBear.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
            services.AddDbContext<DbContextBase, EfDbContext>(
                options =>
                {
                    options.UseMySQL(Configuration.GetConnectionString("Default"));
                    //options.UseSqlServer(
                    //    Configuration.GetConnectionString("Default"),
                    //    option => option.UseRowNumberForPaging());
                });

            return services.AddDefaultProvider(
                options =>
                {
                    options.UseAutofac();
                    options.UseEfCore();
                    options.AddDemoApplication();
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseDefaultApp(
                options =>
                {
                    options.UseAutoMapper();
                });

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
