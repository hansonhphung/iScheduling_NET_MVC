using System;
using System.Collections.Generic;

namespace iScheduling.Models.Shift
{
    public class AddShiftVM
    {
        public string ShiftId { get; set; }
        public IList<DTO.Models.Employee> ListEmployees { get; set; }
        public string AssignedShiftEmployeeId { get; set; }
        public string AssignedShiftEmployeeFullName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? DateOfShift { get; set; }
        public string ShiftStartAt { get; set; }
        public string ShiftEndAt { get; set; }
        public bool IsCalendarView { get; set; }
    }
}