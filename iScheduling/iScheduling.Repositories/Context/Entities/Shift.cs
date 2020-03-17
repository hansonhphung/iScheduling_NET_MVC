using iScheduling.Context.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iScheduling.Repositories.Context.Entities
{
    public class Shift
    {
        [Key]
        public string ShiftId { get; set; }
        public string EmployeeId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime ActualStartTime { get; set; }
        public DateTime ActualEndTime { get; set; }
        [ForeignKey("ShiftId")]
        public virtual DayOffRequest DayOffRequest { get; set; }
        public string Status { get; set; }

    }
}
