using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace CalendarExample.Models
{
    public class SchedulerContext : DbContext
    {
        public DbSet<SchedulerEvent> SchedulerEvents { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<FullName> FullNames { get; set; }
        public DbSet<FullAddress> FullAddresses { get; set; }
        public DbSet<Person> People { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}