using iScheduling.Context.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace iScheduling.Repositories.Context
{
    public class iSchedulingContext : DbContext
    {
        public iSchedulingContext() : base("iSchedulingContext") { }

        public DbSet<Employee> Employees { get; set; }
    }
}