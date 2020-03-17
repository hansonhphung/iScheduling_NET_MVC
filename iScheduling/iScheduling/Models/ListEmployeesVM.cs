using iScheduling.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iScheduling.Models
{
    public class ListEmployeesVM:BaseViewModel<IEnumerable<Employee>>
    {
        public ListEmployeesVM(IList<Employee> lstEmployees) {
            Data = lstEmployees;
        }

        public ListEmployeesVM(bool isSuccess, string message, IList<Employee> lstEmployees) {
            IsSuccess = isSuccess;
            Message = message;
            Data = lstEmployees;
        }
    }
}