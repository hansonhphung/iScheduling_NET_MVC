using System;
using System.Collections.Generic;

namespace iScheduling.Models.Shift
{
    public class AddShiftVM
    {
        public string ShiftId { get; set; }
        public IList<DTO.Models.Employee> ListEmployees { get; set; }
        public string AssignedShiftEmployeeId { get; set; }
        public DateTime DateOfShift { get; set; }
        public DateTime ShiftStartAt { get; set; }
        public DateTime ShiftEndAt { get; set; }
    }
}