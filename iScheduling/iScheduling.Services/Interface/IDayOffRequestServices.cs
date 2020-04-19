using iScheduling.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iScheduling.Services.Interface
{
    public interface IDayOffRequestServices
    {
        bool CreateDayOff(DayOffRequest request);
        DayOffRequest GetRequestById(string requestId);
        bool ApproveRequest(string requestId, string shiftId, string approvedBy, string responseComment);
        bool RejectRequest(string requestId, string shiftId, string rejectedBy, string responseComment);
        IList<DayOffRequest> GetAllPendingRequest();
    }
}
