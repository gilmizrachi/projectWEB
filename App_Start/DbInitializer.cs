using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using projectWEB.Data;
using projectWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectWEB.App_Start
{
    public class DbInitializer : IDbInitializer
    {
        private readonly IServiceScopeFactory _scopeFactory;
        public DbInitializer(IServiceScopeFactory scopeFactory)
        {
            this._scopeFactory = scopeFactory;
        }

        public void Initialize()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<projectWEBContext>())
                {
                    context.Database.Migrate();
                }
            }
        }

        public void SeedData()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<projectWEBContext>())
                {
                    //add admin user
                    if (!context.RegisteredUsers.Any())
                    {
                        SeedRoles(context);
                        //SeedUsers(context);
                        //SeedCategories(context);
                    }
                }
            }
        }
        public async void SeedRoles(projectWEBContext context)
        {
            List<IdentityRole> allRoles = new List<IdentityRole>();

            allRoles.Add(new IdentityRole() { Name = "Administrator" });
            allRoles.Add(new IdentityRole() { Name = "Moderator" });
            allRoles.Add(new IdentityRole() { Name = "User" });

            var rolesStore = new RoleStore<IdentityRole>(context);
            var rolesManager = new RoleManager<IdentityRole>(rolesStore,null,null,null,null);

            foreach (IdentityRole role in allRoles)
            {
                //var roleExist = await rolesManager.RoleExistsAsync(role.Name);
                //if (!roleExist)
                //{
                    IdentityResult result = await rolesManager.CreateAsync(role);

                    if (result.Succeeded) continue;
                //}
            }
        }
        public async void SeedUsers(projectWEBContext context)
        {
            var usersStore = new UserStore<RegisteredUsers>(context);
            var usersManager = new UserManager<RegisteredUsers>(usersStore, null, null, null, null, null, null, null, null);

            RegisteredUsers admin = new RegisteredUsers();
            admin.FullName = "Admin";

            admin.Email = "adm_use@domain.com";
            admin.UserName = "adm_use";
            var password = "adm_use123";

            admin.PhoneNumber = "0534675874";
            admin.Country = "asd";
            admin.City = "daas";
            admin.Address = "asd";
            admin.ZipCode = "123456";

            admin.RegisteredOn = DateTime.Now;

            if (usersManager.FindByEmailAsync(admin.Email) == null)
            {
                IdentityResult result =await usersManager.CreateAsync(admin, password);

                if (result.Succeeded)
                {
                    //add necessary roles to admin
                    await usersManager.AddToRoleAsync(admin, "Administrator");
                    await usersManager.AddToRoleAsync(admin, "Moderator");
                    await usersManager.AddToRoleAsync(admin, "User");
                }
            }
        }
        public void SeedCategories(projectWEBContext context)
        {
            Category uncategorized = new Category()
            {
                SanitizedName = "uncategorized",
                DisplaySeqNo = 0,
                ModifiedOn = DateTime.Now
            };

            CategoryRecord uncategorizedEnRecord = new CategoryRecord()
            {
                Category = uncategorized,
                Name = "Uncategorized",
                Description = "Products that are not categorized. uncategorised, unclassified - not arranged in any specific grouping.",
                ModifiedOn = DateTime.Now
            };

            context.Categories.Add(uncategorized);
            context.CategoryRecords.Add(uncategorizedEnRecord);
            context.SaveChanges();
        }
    }
}
