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
using AutoMapper;
using GreatBear.EntityFramework;
using Microsoft.EntityFrameworkCore;
using GreatBear.Demo.EFCore;
using GreatBear.Demo.Application;
using Microsoft.Extensions.Logging;
using GreatBear.Log4net;
using FluentValidation.AspNetCore;
using GreatBear.Demo.Application.Validators;
using GreatBear.Demo.Application.Validators.Users;
//using GreatBear.Dapper;

namespace GreatBear.Demo.WebApp
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


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(fv=> {
                    fv.RegisterValidatorsFromAssemblyContaining<UserModelValidator>();
                    fv.ImplicitlyValidateChildProperties = true;
                });

            services.AddAuthentication().AddCookie(MemberAttribute.AuthenticationScheme, options =>
            {
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromHours(1);
                options.Cookie.Expiration = TimeSpan.FromHours(1);
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
            })
            .AddCookie(AdminAttribute.AuthenticationScheme, options =>
            {
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromHours(1);
                options.Cookie.Expiration = TimeSpan.FromHours(1);
                options.LoginPath = "/Admin/Account/Login";
                options.LogoutPath = "/Admin/Account/Logout";
            });
            services.AddAutoMapper();



            services.AddDbContext<DbContext, EfDbContext>(
                options =>
                {
                    //options.UseMySQL(Configuration.GetConnectionString("Default"));
                    options.UseSqlServer(
                        Configuration.GetConnectionString("Default"),
                        option => option.UseRowNumberForPaging());
                });

            return services.AddDefaultProvider(
                options =>
                {
                    options.UseAutofac();
                    options.UseEfCore();
                    options.AddLog4net();
                    options.AddDemoApplication();
                    options.AddWebApp();
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            loggerFactory.AddProvider(new Log4NetProvider("log4net.config"));

            app.UseDefaultApp();

            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "area",
                    template: "{area:exists}/{controller=Default}/{action=Index}/{id?}");
            });
        }
    }
}
