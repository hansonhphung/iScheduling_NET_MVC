using iScheduling.Context.Entities;
using iScheduling.Repositories.Context.Entities;
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
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<VacationRequest> VacationRequests { get; set; }
        public DbSet<DayOffRequest> DayOffRequests { get; set; }
    }
}