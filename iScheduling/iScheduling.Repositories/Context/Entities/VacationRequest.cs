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
    public class VacationRequest
    {
        [Key]
        public string RequestId { get; set; }
        public string RequestEmployeeId { get; set; }
        public string ResponseManagerId { get; set; }
        public DateTime RequestedAt { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public string Comment { get; set; }
    }
}
