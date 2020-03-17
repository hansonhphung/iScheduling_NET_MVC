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
        public string AssignedBy { get; set; }
        public DateTime AssignedAt { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}