using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iScheduling.Models.Shift
{
    public class ShiftEmployeeViewVM
    {
        public string EmployeeId { get; set; }
        public string EmployeeFullName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IList<DTO.Models.Shift> ListShifts { get; set; }
    }
}