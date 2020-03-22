using iScheduling.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iScheduling.Services.Interface
{
    public interface  IEmployeeServices : IServices
    {
        IList<Employee> GetAllEmployees();

        IList<Employee> GetAllEmployeeOrderByPosition();

        Employee GetEmployeeById(string empId);

        bool AddEmployee(Employee e);

        bool UpdateEmployee(Employee e);

        bool DeleteEmployee(string empId);
    }
}
