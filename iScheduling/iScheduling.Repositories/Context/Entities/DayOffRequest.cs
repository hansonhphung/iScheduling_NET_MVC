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
    public class DayOffRequest
    {
        [Key]
        public string RequestId { get; set; }
        public string RequestEmployeeId { get; set; }
        public string ResponseManagerId { get; set; }
        public string ShiftId { get; set; }
        
        public string Status { get; set; }
        public string Comment { get; set; }
    }
}
