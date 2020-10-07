using System;
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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using projectWEB.Models;
using projectWEB.App_Start;

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
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddSession(Options => Options.IdleTimeout = TimeSpan.FromMinutes(10));
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                 .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, config =>
                 {
                     config.LoginPath = new PathString("/login");
                 });
            services.AddControllersWithViews();
            services.AddIdentity<RegisteredUsers, IdentityRole>().AddEntityFrameworkStores<projectWEBContext>()
      .AddDefaultTokenProviders().AddClaimsPrincipalFactory<CustomClaimsPrincipalFactory>();
            services.AddScoped<IUserClaimsPrincipalFactory<RegisteredUsers>, CustomClaimsPrincipalFactory>();
            services.AddScoped<IDbInitializer, DbInitializer>();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();
            
            app.UseAuthentication();
            app.UseAuthorization();
            //var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            //using (var scope = scopeFactory.CreateScope())
            //{
            //    var dbInitializer = scope.ServiceProvider.GetService<IDbInitializer>();
            //    dbInitializer.Initialize();
            //    dbInitializer.SeedData();
            //}
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=RegisteredUsers}/{action=login}/{id?}");
            });
            
            
        }
    }
}
