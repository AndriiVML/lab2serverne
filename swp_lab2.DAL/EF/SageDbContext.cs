using swp_lab2.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace swp_lab2.DAL.EF
{
    public class SageDbContext : DbContext
    {
        public DbSet<Sage> Sages { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Order> Orders { get; set; }

        public SageDbContext() : base("name=swplab2")
        {
            this.Configuration.ProxyCreationEnabled = false;
            Database.SetInitializer(new SageDbInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}
