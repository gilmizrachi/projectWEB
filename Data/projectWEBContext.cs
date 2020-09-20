using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using projectWEB.Models;
using projectWEB.Data;

namespace projectWEB.Data
{
    public class projectWEBContext : DbContext
    {
        public projectWEBContext (DbContextOptions<projectWEBContext> options)
            : base(options)
        {
        }

        public DbSet<projectWEB.Models.RegisteredUsers> RegisteredUsers { get; set; }

        public DbSet<projectWEB.Data.Item> Item { get; set; }

        public DbSet<projectWEB.Data.Category> Category { get; set; }
    }
}
