using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AOWebApp.Models.CodeFirst;

namespace AOWebApp.Data
{
    public class AOWebAppContext : DbContext
    {
        public AOWebAppContext (DbContextOptions<AOWebAppContext> options)
            : base(options)
        {
        }

        public DbSet<AOWebApp.Models.CodeFirst.ExampleItem> ExampleItem { get; set; } = default!;
    }
}
