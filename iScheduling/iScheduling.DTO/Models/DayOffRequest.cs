using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iScheduling.DTO.Models
{
    public class DayOffRequest
    {
        public string RequestId { get; set; }
        public string RequestEmployeeId { get; set; }
        public string RequestEmployeeName { get; set; }
        public string ResponseManagerId { get; set; }
        public string RequestedShiftId { get; set; }
        public Shift RequestedShift { get; set; }
        public DateTime RequestedAt { get; set; }
        public DateTime ResponsedAt { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
        public string ResponseComment { get; set; }
    }
}
