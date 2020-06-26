using iScheduling.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iScheduling.Repositories.Interface
{
    public interface IEmployeeRepositories : IRepositories
    {
        Employee Login(string username, string password);
        bool IsUsernameExists(string username);

        IList<Employee> GetAllEmployees();

        IList<Employee> GetAllEmployeeToAssignShift(DateTime dateOfShift);

        Employee GetEmployeeById(string empId);

        bool AddEmployee(Employee emp);

        bool UpdateEmployee(Employee emp);

        bool DeleteEmployee(string empId);
    }
}
