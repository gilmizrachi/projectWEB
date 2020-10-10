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
    public class projectWEBContext : IdentityDbContext<RegisteredUsers, IdentityRole,string>
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

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductRecord> ProductRecords { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryRecord> CategoryRecords { get; set; }
        public DbSet<Promo> Promos { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<ProductPicture> ProductPictures { get; set; }
        public DbSet<ProductSpecification> ProductSpecifications { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<Configuration> Configurations { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<NewsletterSubscription> NewsletterSubscriptions { get; set; }
        public DbSet<OrderHistory> OrderHistories { get; set; }
        public DbSet<Location> Locations { get; set; }

    }
}
