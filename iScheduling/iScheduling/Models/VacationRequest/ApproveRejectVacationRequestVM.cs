using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iScheduling.Models.VacationRequest
{
    public class ApproveRejectVacationRequestVM
    {
        public string RequestId { get; set; }
        public string RequestedBy { get; set; }
        public DateTime RequestedAt { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ApproveRejectBy { get; set; }
        public string ResponseComment { get; set; }
    }
}