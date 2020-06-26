using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iScheduling.DTO.Models
{
    public class Shift
    {
        public string ShiftId { get; set; }
        public string EmployeeId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DayOffRequest DayOffRequest { get; set; }
        public bool IsDelete { get; set; }
        public bool IsCancel { get; set; }
    }
}