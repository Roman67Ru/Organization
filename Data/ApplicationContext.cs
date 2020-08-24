using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<House> Houses { get; set; }
        public DbSet<Office> Offices { get; set; }
        public DbSet<Equipment> Equipment { get; set; }    
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS01;Database=OrganizationDB;Trusted_Connection=True;");

        }
    }
}
