﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using projectWEB.Data;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace projectWEB
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
            services.AddControllersWithViews();

            services.AddDbContext<projectWEBContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("projectWEBContext")));
            services.AddSession(Options => Options.IdleTimeout = TimeSpan.FromMinutes(10));
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => 
            {
                 options.LoginPath = "/";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.Cookie.Name = "ProjectWeb";
               // options.Cookie.Expiration = TimeSpan.FromMinutes(30);
            });
            services.ConfigureApplicationCookie(opts => opts.LoginPath = "/");


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                //app.UseStatusCodePagesWithReExecute("/error/{0}");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            app.UseAuthentication();

            app.UseAuthorization();
            
          //  app.UseCookiePolicy();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=RegisteredUsers}/{action=login}/{id?}");
            });
            
            
        }
    }
}
