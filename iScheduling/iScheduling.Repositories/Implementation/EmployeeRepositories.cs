using iScheduling.Context.Entities;
using iScheduling.Repositories.Context;
using iScheduling.Repositories.Implementation;
using iScheduling.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iScheduling.Repositories.Implementation
{
    public class EmployeeRepositories : BaseRepositories, IEmployeeRepositories
    {
        public EmployeeRepositories(iSchedulingContext context) : base(context)  { } 
        public IList<Employee> GetAllEmployees()
        {
            var lstEmployees = Entities.Employees.ToList();
            return lstEmployees;
        }
    }
}