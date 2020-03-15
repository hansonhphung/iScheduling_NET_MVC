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
        IList<Employee> GetAllEmployees();
    }
}
