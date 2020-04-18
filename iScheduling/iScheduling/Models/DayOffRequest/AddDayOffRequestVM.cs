using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iScheduling.Models.DayOffRequest
{
    public class AddDayOffRequestVM
    {
        public string ShiftId { get; set; }
        public DateTime DateOfShift { get; set; }
        public string ShiftStartAt { get; set; }
        public string ShiftEndAt { get; set; }
        public string Reason { get; set; }
    }
}