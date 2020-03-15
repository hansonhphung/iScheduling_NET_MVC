using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iScheduling.DTO.Models
{
    public class Shift
    {
        public Guid ShiftId { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid AssignedBy { get; set; }
        public DateTime AssignedAt { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}