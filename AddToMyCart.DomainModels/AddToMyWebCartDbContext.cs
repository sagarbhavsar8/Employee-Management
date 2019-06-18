using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddToMyCart.DomainModels
{
    public class AddToMyWebCartDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Calendar> Calendars { get; set; }
        public DbSet<SetPriority> SetPriorities { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
