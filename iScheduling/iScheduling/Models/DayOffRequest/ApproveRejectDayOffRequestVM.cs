using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iScheduling.Models.DayOffRequest
{
    public class ApproveRejectDayOffRequestVM
    {
        public string ShiftId { get; set; }
        public string AssignedEmployee { get; set; }
        public DateTime DateOfShift { get; set; }
        public string ShiftStartAt { get; set; }
        public string ShiftEndAt { get; set; }
        public string ApproveRejectBy { get; set; }
        public string ResponseComment { get; set; }
    }
}