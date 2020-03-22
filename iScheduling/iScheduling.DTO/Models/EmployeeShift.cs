using System;

namespace iScheduling.DTO.Models
{
    public class EmployeeShift
    {
        // Open at 9 AM
        private static int OPENING_HOUR = 9;
        private static int CLOSING_HOUR = 20;

        public string ShiftId { get; set; }
        public string EmployeeId { get; set; }
        public string FullName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime DateOfShift { get; set; }
        public int NonWorking_1 {
            get {
                return StartTime.Hour - OPENING_HOUR;
            }
        }
        public int Working {
            get {
                return EndTime.Hour - StartTime.Hour;
            }
        }
        public int NonWorking_2 {
            get {
                return CLOSING_HOUR - EndTime.Hour;
            }
        }
        public string ColorFrom { get; set; }
        public string ColorTo { get; set; }
    }
}