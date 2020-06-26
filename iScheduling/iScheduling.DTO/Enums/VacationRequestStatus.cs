using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iScheduling.DTO.Enums
{
    public enum VacationRequestStatus
    {
        [Description("Pending")]
        PENDING,
        [Description("Approved")]
        APPROVED,
        [Description("Rejected")]
        REJECTED
    }
}
