using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iScheduling.Models.VacationRequest
{
    public class ListVacationRequestVM
    {
        public string KeyWord { get; set; }
        public IList<DTO.Models.VacationRequest> ListRequest;
    }
}