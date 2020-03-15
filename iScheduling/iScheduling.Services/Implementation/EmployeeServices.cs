using iScheduling.Context;
using iScheduling.DTO.Models;
using iScheduling.Repositories.Interface;
using iScheduling.Services.Implementation;
using iScheduling.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iScheduling.Implementation.Services
{
    public class EmployeeServices : BaseServices, IEmployeeServices
    {
        private readonly IEmployeeRepositories employeeRepositories;
        public EmployeeServices(IEmployeeRepositories _employeeRepository){
            employeeRepositories = _employeeRepository;
        }

        public IList<Employee> GetAllEmployees()
        {
            return employeeRepositories.GetAllEmployees().Select(x => new Employee()).ToList();
        }
    }
}