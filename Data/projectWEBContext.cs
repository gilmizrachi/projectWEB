using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using projectWEB.Models;
using projectWEB.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace projectWEB.Data
{
    public class projectWEBContext : IdentityDbContext<RegisteredUsers>
    {
        public projectWEBContext(DbContextOptions<projectWEBContext> options)
         : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // This needs to go before the other rules!

            modelBuilder.Entity<Promo>()
                .HasIndex(p => new { p.Code })
                .IsUnique(true);

            modelBuilder.Entity<RegisteredUsers>().ToTable("Users");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
        }
        public DbSet<RegisteredUsers> RegisteredUsers { get; set; }

        public DbSet<Item> Item { get; set; }

        public DbSet<Category> Category { get; set; }
        public DbSet<CategoryRecord> CategoryRecords { get; set; }

        public DbSet<Order> Order { get; set; }

        public DbSet<Location> Location { get; set; }
    }
}
