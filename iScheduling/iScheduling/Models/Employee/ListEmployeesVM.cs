using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iScheduling.Models.Employee
{
    public class ListEmployeesVM:BaseViewModel<IEnumerable<DTO.Models.Employee>>
    {
        public ListEmployeesVM() : base(false, string.Empty, null) {
        }

        public ListEmployeesVM(bool isSuccess, string message, IList<DTO.Models.Employee> lstEmployees) : base(isSuccess, message, lstEmployees){
        }
    }
}